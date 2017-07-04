using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Power_Control
{
    class HandsRecognition
    {
        PXCMImage.ImageInfo _info;
        readonly byte[] _lut;
        private readonly MainForm _form;
        private bool _disconnected = false;
        //Queue containing depth image - for synchronization purposes
        private readonly Queue<PXCMImage> _mImages;

        private readonly Queue<PXCMPoint3DF32>[] _mCursorPoints;
        private readonly Queue<PXCMPoint3DF32>[] _mAdaptivePoints;
        private readonly int[] _mCursorClick;
        private readonly PXCMCursorData.BodySideType[] _mCursorHandSide;
        private const int NumberOfFramesToDelay = 3;

        private float _maxRange;


        public HandsRecognition(MainForm form)
        {
            _mImages = new Queue<PXCMImage>();
            _mCursorPoints = new Queue<PXCMPoint3DF32>[2];
            _mCursorPoints[0] = new Queue<PXCMPoint3DF32>();
            _mCursorPoints[1] = new Queue<PXCMPoint3DF32>();

            _mAdaptivePoints = new Queue<PXCMPoint3DF32>[2];
            _mAdaptivePoints[0] = new Queue<PXCMPoint3DF32>();
            _mAdaptivePoints[1] = new Queue<PXCMPoint3DF32>();
            _mCursorHandSide = new PXCMCursorData.BodySideType[2];

            _mCursorClick = new int[2];

            this._form = form;
            _lut = Enumerable.Repeat((byte)0, 256).ToArray();
            _lut[255] = 1;
        }




        /* Checking if sensor device connect or not */
        private bool DisplayDeviceConnection(bool state)
        {
            if (state)
            {
                if (!_disconnected) _form.UpdateStatus("Device Disconnected");
                _disconnected = true;
            }
            else
            {
                if (_disconnected) _form.UpdateStatus("Device Reconnected");
                _disconnected = false;
            }
            return _disconnected;
        }

        /* Displaying current frame gestures */
        private void DisplayGesture(PXCMHandData handAnalysis, int frameNumber)
        {

            int firedGesturesNumber = handAnalysis.QueryFiredGesturesNumber();
            string gestureStatusLeft = string.Empty;
            string gestureStatusRight = string.Empty;

            if (firedGesturesNumber == 0)
            {
                return;
            }

            for (int i = 0; i < firedGesturesNumber; i++)
            {
                PXCMHandData.GestureData gestureData;
                if (handAnalysis.QueryFiredGestureData(i, out gestureData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    PXCMHandData.IHand handData;
                    if (handAnalysis.QueryHandDataById(gestureData.handId, out handData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                        return;

                    PXCMHandData.BodySideType bodySideType = handData.QueryBodySide();
                    if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_LEFT)
                    {
                        gestureStatusLeft += "Left Hand Gesture: " + gestureData.name;
                    }
                    else if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_RIGHT)
                    {
                        gestureStatusRight += "Right Hand Gesture: " + gestureData.name;
                    }

                }

            }
            if (gestureStatusLeft == String.Empty)
                _form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusRight + "\n", Color.SeaGreen);
            else
                _form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusLeft + ", " + gestureStatusRight + "\n", Color.SeaGreen);

        }

        /* Displaying current frame gestures */
        private void DisplayCursorGesture(PXCMCursorData cursorAnalysis, int frameNumber)
        {

            int firedGesturesNumber = cursorAnalysis.QueryFiredGesturesNumber();
            string gestureStatusLeft = string.Empty;
            string gestureStatusRight = string.Empty;

            if (firedGesturesNumber == 0)
            {
                return;
            }

            for (int i = 0; i < firedGesturesNumber; i++)
            {
                PXCMCursorData.GestureData gestureData;
                if (cursorAnalysis.QueryFiredGestureData(i, out gestureData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    PXCMCursorData.ICursor cursorData;
                    if (cursorAnalysis.QueryCursorDataById(gestureData.handId, out cursorData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                        return;

                    PXCMCursorData.BodySideType bodySideType = cursorData.QueryBodySide();

                    string gestureName = string.Empty;
                    switch (gestureData.label)
                    {
                        case PXCMCursorData.GestureType.CURSOR_CLICK:
                            gestureName = "CURSOR_CLICK";
                            break;
                        case PXCMCursorData.GestureType.CURSOR_CLOCKWISE_CIRCLE:
                            gestureName = "CURSOR_CLOCKWISE_CIRCLE";
                            break;
                        case PXCMCursorData.GestureType.CURSOR_COUNTER_CLOCKWISE_CIRCLE:
                            gestureName = "CURSOR_COUNTER_CLOCKWISE_CIRCLE";
                            break;
                        case PXCMCursorData.GestureType.CURSOR_HAND_OPENING:
                            gestureName = "CURSOR_HAND_OPENING";
                            break;
                        case PXCMCursorData.GestureType.CURSOR_HAND_CLOSING:
                            gestureName = "CURSOR_HAND_CLOSING";
                            break;

                    }


                    if (!string.IsNullOrEmpty(gestureName))
                    {
                        if (bodySideType == PXCMCursorData.BodySideType.BODY_SIDE_LEFT)
                        {
                            gestureStatusLeft += "Left Hand Gesture: " + gestureName;
                        }
                        else if (bodySideType == PXCMCursorData.BodySideType.BODY_SIDE_RIGHT)
                        {
                            gestureStatusRight += "Right Hand Gesture: " + gestureName;
                        }
                    }


                }

            }
            if (gestureStatusLeft == String.Empty)
                _form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusRight + "\n", Color.SeaGreen);
            else
                _form.UpdateInfo("Frame " + frameNumber + ") " + gestureStatusLeft + ", " + gestureStatusRight + "\n", Color.SeaGreen);

        }

        /* Displaying Depth/Mask Images - for depth image only we use a delay of NumberOfFramesToDelay to sync image with tracking */
        private unsafe void DisplayPicture(PXCMImage depth, PXCMHandData handAnalysis)
        {
            if (depth == null)
                return;

            PXCMImage image = depth;


            //Mask Image
            if (_form.GetLabelmapState())
            {
                Bitmap labeledBitmap = null;
                try
                {
                    int numOfHands = handAnalysis.QueryNumberOfHands();

                    PXCMPointI32[][] pointOuter = new PXCMPointI32[numOfHands][];
                    PXCMPointI32[][] pointInner = new PXCMPointI32[numOfHands][];

                    labeledBitmap = new Bitmap(image.info.width, image.info.height, PixelFormat.Format32bppRgb);
                    for (int j = 0; j < numOfHands; j++)
                    {
                        int id;
                        PXCMImage.ImageData data;

                        handAnalysis.QueryHandId(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_TIME, j, out id);
                        //Get hand by time of appearance
                        PXCMHandData.IHand handData;
                        handAnalysis.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_TIME, j, out handData);
                        if (handData != null &&
                            (handData.QuerySegmentationImage(out image) >= pxcmStatus.PXCM_STATUS_NO_ERROR))
                        {
                            if (image.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_Y8,
                                out data) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                            {
                                Rectangle rect = new Rectangle(0, 0, image.info.width, image.info.height);

                                BitmapData bitmapdata = labeledBitmap.LockBits(rect, ImageLockMode.ReadWrite, labeledBitmap.PixelFormat);
                                byte* numPtr = (byte*)bitmapdata.Scan0; //dst
                                byte* numPtr2 = (byte*)data.planes[0]; //row
                                int imagesize = image.info.width * image.info.height;
                                byte num2 = (_form.GetFullHandModeState()) ? (byte)handData.QueryBodySide() : (byte)1;

                                byte tmp = 0;
                                for (int i = 0; i < imagesize; i++, numPtr += 4, numPtr2++)
                                {
                                    tmp = (byte)(_lut[numPtr2[0]] * num2 * 100);
                                    numPtr[0] = (Byte)(tmp | numPtr[0]);
                                    numPtr[1] = (Byte)(tmp | numPtr[1]);
                                    numPtr[2] = (Byte)(tmp | numPtr[2]);
                                    numPtr[3] = 0xff;
                                }

                                labeledBitmap.UnlockBits(bitmapdata);
                                image.ReleaseAccess(data);

                            }

                            if ((_form.GetContourState()))
                            {
                                int contourNumber = handData.QueryNumberOfContours();
                                if (contourNumber > 0)
                                {
                                    for (int k = 0; k < contourNumber; ++k)
                                    {
                                        PXCMHandData.IContour contour;
                                        pxcmStatus sts = handData.QueryContour(k, out contour);
                                        if (sts == pxcmStatus.PXCM_STATUS_NO_ERROR)
                                        {
                                            if (contour.IsOuter() == true)
                                                contour.QueryPoints(out pointOuter[j]);
                                            else
                                            {
                                                contour.QueryPoints(out pointInner[j]);
                                            }
                                        }
                                    }

                                }
                            }

                        }
                    }
                    if (labeledBitmap != null)
                    {

                        _form.DisplayBitmap(labeledBitmap);
                        labeledBitmap.Dispose();
                    }
                    image.Dispose();

                    for (int i = 0; i < numOfHands; i++)
                    {
                        if (_form.GetContourState())
                        {
                            if (pointOuter[i] != null && pointOuter[i].Length > 0)
                                _form.DisplayContour(pointOuter[i], i);
                            if (pointInner[i] != null && pointInner[i].Length > 0)
                                _form.DisplayContour(pointInner[i], i);
                        }

                    }

                }
                catch (Exception)
                {
                    if (labeledBitmap != null)
                    {
                        labeledBitmap.Dispose();
                    }
                    if (image != null)
                    {
                        image.Dispose();
                    }
                }

            }//end label image

            //Depth Image
            else
            {
                //collecting 3 images inside a queue and displaying the oldest image
                PXCMImage.ImageInfo info;
                PXCMImage image2;

                info = image.QueryInfo();
                image2 = _form.g_session.CreateImage(info);
                if (image2 == null) { return; }
                image2.CopyImage(image);
                _mImages.Enqueue(image2);
                if (_mImages.Count == NumberOfFramesToDelay)
                {
                    Bitmap depthBitmap;
                    try
                    {
                        depthBitmap = new Bitmap(image.info.width, image.info.height, PixelFormat.Format32bppRgb);
                    }
                    catch (Exception)
                    {
                        image.Dispose();
                        PXCMImage queImage = _mImages.Dequeue();
                        queImage.Dispose();
                        return;
                    }

                    PXCMImage.ImageData data3;
                    PXCMImage image3 = _mImages.Dequeue();
                    if (image3.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_DEPTH, out data3) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        float fMaxValue = _maxRange;
                        byte cVal;

                        Rectangle rect = new Rectangle(0, 0, image.info.width, image.info.height);
                        BitmapData bitmapdata = depthBitmap.LockBits(rect, ImageLockMode.ReadWrite, depthBitmap.PixelFormat);

                        byte* pDst = (byte*)bitmapdata.Scan0;
                        short* pSrc = (short*)data3.planes[0];
                        int size = image.info.width * image.info.height;

                        for (int i = 0; i < size; i++, pSrc++, pDst += 4)
                        {
                            cVal = (byte)((*pSrc) / fMaxValue * 255);
                            if (cVal != 0)
                                cVal = (byte)(255 - cVal);

                            pDst[0] = cVal;
                            pDst[1] = cVal;
                            pDst[2] = cVal;
                            pDst[3] = 255;
                        }
                        try
                        {
                            depthBitmap.UnlockBits(bitmapdata);
                        }
                        catch (Exception)
                        {
                            image3.ReleaseAccess(data3);
                            depthBitmap.Dispose();
                            image3.Dispose();
                            return;
                        }

                        _form.DisplayBitmap(depthBitmap);
                        image3.ReleaseAccess(data3);
                    }
                    depthBitmap.Dispose();
                    image3.Dispose();
                }
            }

        }

        /* Displaying Depth/Mask Images - for depth image only we use a delay of NumberOfFramesToDelay to sync image with tracking */
        private unsafe void DisplayCursorPicture(PXCMImage depth, PXCMCursorData cursorAnalysis)
        {
            if (depth == null)
                return;

            PXCMImage image = depth;
            _info = image.QueryInfo();
            Bitmap depthBitmap;
            depthBitmap = new Bitmap(image.info.width, image.info.height, PixelFormat.Format8bppIndexed);
            _form.DisplayBitmap(depthBitmap);
            depthBitmap.Dispose();


        }

        /* Displaying current frames hand joints */
        private void DisplayJoints(PXCMHandData handOutput, long timeStamp = 0)
        {


            if (_form.GetJointsState() || _form.GetSkeletonState() || _form.GetExtremitiesState())
            {
                //Iterate hands
                PXCMHandData.JointData[][] nodes = new PXCMHandData.JointData[][] { new PXCMHandData.JointData[0x20], new PXCMHandData.JointData[0x20] };
                PXCMHandData.ExtremityData[][] extremityNodes = new PXCMHandData.ExtremityData[][] { new PXCMHandData.ExtremityData[0x6], new PXCMHandData.ExtremityData[0x6] };

                int numOfHands = handOutput.QueryNumberOfHands();

                if (numOfHands == 1) _mCursorPoints[1].Clear();

                for (int i = 0; i < numOfHands; i++)
                {
                    //Get hand by time of appearance
                    PXCMHandData.IHand handData;
                    if (handOutput.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_TIME, i, out handData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        if (handData != null)
                        {
                            //Iterate Joints
                            for (int j = 0; j < 0x20; j++)
                            {
                                PXCMHandData.JointData jointData;
                                handData.QueryTrackedJoint((PXCMHandData.JointType)j, out jointData);
                                nodes[i][j] = jointData;

                            } // end iterating over joints



                            // iterate over extremity points
                            if (_form.GetExtremitiesState())
                            {
                                for (int j = 0; j < PXCMHandData.NUMBER_OF_EXTREMITIES; j++)
                                {
                                    handData.QueryExtremityPoint((PXCMHandData.ExtremityType)j, out extremityNodes[i][j]);
                                }
                            }

                        }
                    }
                } // end iterating over hands


                _form.DisplayJoints(nodes, numOfHands);
                if (numOfHands > 0)
                {

                    if (_form.GetCursorModeState())
                        _form.DisplayCursor(numOfHands, _mCursorPoints, _mAdaptivePoints, _mCursorClick, _mCursorHandSide);

                    if (_form.GetExtremitiesState())
                        _form.DisplayExtremities(numOfHands, extremityNodes);
                }

            }
        }

        /* Displaying current frames hand joints */
        private void DisplayCursorJoints(PXCMCursorData cursorOutput, long timeStamp = 0)
        {
            _mCursorClick[0] = Math.Max(0, _mCursorClick[0] - 1);
            _mCursorClick[1] = Math.Max(0, _mCursorClick[1] - 1);


            int numOfHands = cursorOutput.QueryNumberOfCursors();
            if (numOfHands == 1)
            {
                _mCursorPoints[1].Clear();
                _mAdaptivePoints[1].Clear();
            }

            for (int i = 0; i < numOfHands; i++)
            {
                //Get hand by time of appearance
                PXCMCursorData.ICursor cursorData;
                if (cursorOutput.QueryCursorData(PXCMCursorData.AccessOrderType.ACCESS_ORDER_BY_TIME, i, out cursorData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    if (cursorData != null)
                    {
                        // collect cursor points
                        if (_form.GetCursorState())
                        {
                            PXCMPoint3DF32 imagePoint = cursorData.QueryCursorPointImage();


                            _mCursorPoints[i].Enqueue(imagePoint);
                            if (_mCursorPoints[i].Count > 50)
                                _mCursorPoints[i].Dequeue();
                        }

                        // collect adaptive points
                        if (_form.GetAdaptiveState())
                        {
                            PXCMPoint3DF32 adaptivePoint = cursorData.QueryAdaptivePoint();
                            adaptivePoint.x *= 640;
                            adaptivePoint.y = adaptivePoint.y * 480;



                            _mAdaptivePoints[i].Enqueue(adaptivePoint);
                            if (_mAdaptivePoints[i].Count > 50)
                                _mAdaptivePoints[i].Dequeue();
                        }
                        _mCursorHandSide[i] = cursorData.QueryBodySide();
                        PXCMCursorData.GestureData gestureData;
                        if (cursorOutput.IsGestureFiredByHand(PXCMCursorData.GestureType.CURSOR_CLICK, cursorData.QueryUniqueId(), out gestureData))
                        {
                            _mCursorClick[i] = 7;
                        }
                    }
                }
            } // end iterating over hands


            if (numOfHands > 0)
            {

                if (_form.GetCursorModeState())
                    _form.DisplayCursor(numOfHands, _mCursorPoints, _mAdaptivePoints, _mCursorClick, _mCursorHandSide);
            }
            else
            {
                _mCursorPoints[0].Clear();
                _mCursorPoints[1].Clear();
                _mAdaptivePoints[0].Clear();
                _mAdaptivePoints[1].Clear();
                _mCursorHandSide[0] = PXCMCursorData.BodySideType.BODY_SIDE_UNKNOWN;
                _mCursorHandSide[1] = PXCMCursorData.BodySideType.BODY_SIDE_UNKNOWN;
            }
        }


        /* Displaying current frame alerts */
        private void DisplayAlerts(PXCMHandData handAnalysis, int frameNumber)
        {
            bool isChanged = false;
            string sAlert = "Alert: ";
            for (int i = 0; i < handAnalysis.QueryFiredAlertsNumber(); i++)
            {
                PXCMHandData.AlertData alertData;
                if (handAnalysis.QueryFiredAlertData(i, out alertData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                    continue;

                //See PXCMHandAnalysis.AlertData.AlertType for all available alerts
                switch (alertData.label)
                {
                    case PXCMHandData.AlertType.ALERT_HAND_DETECTED:
                        {

                            sAlert += "Hand Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_NOT_DETECTED:
                        {

                            sAlert += "Hand Not Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_CALIBRATED:
                        {

                            sAlert += "Hand Calibrated, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_NOT_CALIBRATED:
                        {

                            sAlert += "Hand Not Calibrated, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_INSIDE_BORDERS:
                        {

                            sAlert += "Hand Inside Border, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BORDERS:
                        {

                            sAlert += "Hand Out Of Borders, ";
                            isChanged = true;
                            break;
                        }

                }
            }
            if (isChanged)
            {
                _form.UpdateInfo("Frame " + frameNumber + ") " + sAlert + "\n", Color.RoyalBlue);
            }
        }

        /* Displaying current frame alerts */
        private void DisplayCursorAlerts(PXCMCursorData cursorAnalysis, int frameNumber)
        {
            bool isChanged = false;
            string sAlert = "Alert: ";
            for (int i = 0; i < cursorAnalysis.QueryFiredAlertsNumber(); i++)
            {
                PXCMCursorData.AlertData alertData;
                if (cursorAnalysis.QueryFiredAlertData(i, out alertData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                    continue;

                //See PXCMCursorData.AlertType for all available alerts
                switch (alertData.label)
                {

                    case PXCMCursorData.AlertType.CURSOR_DETECTED:
                        {

                            sAlert += "Cursor Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMCursorData.AlertType.CURSOR_NOT_DETECTED:
                        {

                            sAlert += "Cursor Not Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMCursorData.AlertType.CURSOR_INSIDE_BORDERS:
                        {

                            sAlert += "Cursor inside Borders, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMCursorData.AlertType.CURSOR_OUT_OF_BORDERS:
                        {

                            sAlert += "Cursor Out Of Borders, ";
                            isChanged = true;
                            break;
                        }
                }
            }
            if (isChanged)
            {
                _form.UpdateInfo("Frame " + frameNumber + ") " + sAlert + "\n", Color.RoyalBlue);
            }
        }

        public static pxcmStatus OnNewFrame(Int32 mid, PXCMBase module, PXCMCapture.Sample sample)
        {
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }


        /* Using PXCMSenseManager to handle data */
        public void SimplePipeline()
        {
            _form.UpdateInfo(String.Empty, Color.Black);
            bool liveCamera = false;

            bool flag = true;
            PXCMSenseManager instance = null;
            _disconnected = false;
            instance = _form.g_session.CreateSenseManager();
            if (instance == null)
            {
                _form.UpdateStatus("Failed creating SenseManager");
                _form.EnableTrackingMode(true);
                return;
            }

            PXCMCaptureManager captureManager = instance.captureManager;
            PXCMCapture.DeviceInfo info = null;
            if (captureManager != null)
            {
                if (_form.GetPlaybackState())
                {
                    captureManager.SetFileName(_form.GetFileName(), false);
                    info = _form.GetDeviceFromFileMenu(_form.GetFileName());
                }
                else
                {
                    if (_form == null || _form.Devices.Count == 0)
                    {
                        _form.UpdateStatus("No device were found");
                        return;
                    }

                    _form.Devices.TryGetValue(_form.GetCheckedDevice(), out info);


                    if (_form.GetRecordState())
                    {
                        captureManager.SetFileName(_form.GetFileName(), true);
                        if (_form.Devices.TryGetValue(_form.GetCheckedDevice(), out info))
                        {
                            captureManager.FilterByDeviceInfo(_form.GetCheckedDeviceInfo());
                        }

                    }
                    else
                    {
                        captureManager.FilterByDeviceInfo(_form.GetCheckedDeviceInfo());
                        liveCamera = true;
                    }
                    if (info == null)
                    {
                        _form.UpdateStatus("Device Failure");
                        return;
                    }
                }
            }

            /* Set Module */

            PXCMHandModule handAnalysis;
            PXCMHandCursorModule handCursorAnalysis;

            PXCMSenseManager.Handler handler = new PXCMSenseManager.Handler();
            handler.onModuleProcessedFrame = new PXCMSenseManager.Handler.OnModuleProcessedFrameDelegate(OnNewFrame);


            PXCMHandConfiguration handConfiguration = null;
            PXCMHandData handData = null;
            PXCMCursorConfiguration cursorConfiguration = null;
            PXCMCursorData cursorData = null;

            if (_form.GetCursorModeState())
            {
                if (info == null)
                {
                    _form.UpdateStatus("Device Failure");
                    return;
                }
                if (info.model != PXCMCapture.DeviceModel.DEVICE_MODEL_SR300)
                {
                    _form.UpdateStatus(_form.GetPlaybackState()
                        ? "Cursor mode is unsupported for chosen playback file"
                        : "Cursor mode is unsupported for chosen device");
                    return;
                }

                pxcmStatus status = instance.EnableHandCursor();
                handCursorAnalysis = instance.QueryHandCursor();

                if (status != pxcmStatus.PXCM_STATUS_NO_ERROR || handCursorAnalysis == null)
                {
                    _form.UpdateStatus("Failed Loading Module");
                    _form.EnableTrackingMode(true);

                    return;
                }
                cursorConfiguration = handCursorAnalysis.CreateActiveConfiguration();

                if (cursorConfiguration == null)
                {
                    _form.UpdateStatus("Failed Create Configuration");
                    _form.EnableTrackingMode(true);
                    instance.Close();
                    instance.Dispose();
                    return;
                }
                cursorData = handCursorAnalysis.CreateOutput();
                if (cursorData == null)
                {
                    _form.UpdateStatus("Failed Create Output");
                    _form.EnableTrackingMode(true);
                    instance.Close();
                    instance.Dispose();
                    return;
                }

            }
            else
            {
                pxcmStatus status = instance.EnableHand();
                handAnalysis = instance.QueryHand();

                if (status != pxcmStatus.PXCM_STATUS_NO_ERROR || handAnalysis == null)
                {
                    _form.UpdateStatus("Failed Loading Module");
                    _form.EnableTrackingMode(true);

                    return;
                }

                handConfiguration = handAnalysis.CreateActiveConfiguration();
                if (handConfiguration == null)
                {
                    _form.UpdateStatus("Failed Create Configuration");
                    _form.EnableTrackingMode(true);
                    instance.Close();
                    instance.Dispose();
                    return;
                }
                handData = handAnalysis.CreateOutput();
                if (handData == null)
                {
                    _form.UpdateStatus("Failed Create Output");
                    _form.EnableTrackingMode(true);
                    handConfiguration.Dispose();
                    instance.Close();
                    instance.Dispose();
                    return;
                }

            }
            FPSTimer timer = new FPSTimer(_form);
            _form.UpdateStatus("Init Started");
            if (instance.Init(handler) == pxcmStatus.PXCM_STATUS_NO_ERROR)
            {

                PXCMCapture.DeviceInfo dinfo;
                PXCMCapture.DeviceModel dModel = PXCMCapture.DeviceModel.DEVICE_MODEL_F200;
                PXCMCapture.Device device = instance.captureManager.device;
                if (device != null)
                {
                    device.QueryDeviceInfo(out dinfo);
                    dModel = dinfo.model;
                    _maxRange = device.QueryDepthSensorRange().max;

                }

                if (_form.GetCursorModeState())
                {
                    if (cursorConfiguration != null)
                    {
                        cursorConfiguration.EnableAllAlerts();
                        cursorConfiguration.ApplyChanges();
                    }

                    _form.resetGesturesList();
                    this._form.UpdateGesturesToList("", 0);
                    this._form.UpdateGesturesToList("cursor_click", 1);
                    this._form.UpdateGesturesToList("cursor_clockwise_circle", 2);
                    this._form.UpdateGesturesToList("cursor_counterclockwise_circle", 3);
                    this._form.UpdateGesturesToList("cursor_hand_opening", 4);
                    this._form.UpdateGesturesToList("cursor_hand_closing", 5);

                    _form.UpdateGesturesListSize();

                }
                else
                {
                    if (handConfiguration != null)
                    {
                        PXCMHandData.TrackingModeType trackingMode = PXCMHandData.TrackingModeType.TRACKING_MODE_FULL_HAND;

                        if (_form.GetFullHandModeState())
                            trackingMode = PXCMHandData.TrackingModeType.TRACKING_MODE_FULL_HAND;

                        handConfiguration.SetTrackingMode(trackingMode);

                        handConfiguration.EnableAllAlerts();
                        handConfiguration.EnableSegmentationImage(true);
                        bool isEnabled = handConfiguration.IsSegmentationImageEnabled();

                        handConfiguration.ApplyChanges();


                        _form.resetGesturesList();
                        int totalNumOfGestures = handConfiguration.QueryGesturesTotalNumber();

                        if (totalNumOfGestures > 0)
                        {
                            this._form.UpdateGesturesToList("", 0);
                            for (int i = 0; i < totalNumOfGestures; i++)
                            {
                                string gestureName = string.Empty;
                                if (handConfiguration.QueryGestureNameByIndex(i, out gestureName) ==
                                    pxcmStatus.PXCM_STATUS_NO_ERROR)
                                {
                                    this._form.UpdateGesturesToList(gestureName, i + 1);
                                }
                            }

                            _form.UpdateGesturesListSize();
                        }


                    }
                }


                _form.UpdateStatus("Streaming");
                int frameCounter = 0;
                int frameNumber = 0;

                while (!_form.stop)
                {
                    if (cursorConfiguration != null && _form.GetCursorModeState())
                    {
                        string gestureName = _form.GetGestureName();
                        if (string.IsNullOrEmpty(gestureName) == false)
                        {
                            cursorConfiguration.DisableAllGestures();
                            switch (gestureName)
                            {
                                case "cursor_click":
                                    if (cursorConfiguration.IsGestureEnabled(PXCMCursorData.GestureType.CURSOR_CLICK) == false)
                                    {
                                        cursorConfiguration.EnableGesture(PXCMCursorData.GestureType.CURSOR_CLICK);
                                    }
                                    break;
                                case "cursor_clockwise_circle":
                                    if (cursorConfiguration.IsGestureEnabled(PXCMCursorData.GestureType.CURSOR_CLOCKWISE_CIRCLE) == false)
                                    {
                                        cursorConfiguration.EnableGesture(PXCMCursorData.GestureType.CURSOR_CLOCKWISE_CIRCLE);
                                    }
                                    break;
                                case "cursor_counterclockwise_circle":
                                    if (cursorConfiguration.IsGestureEnabled(PXCMCursorData.GestureType.CURSOR_COUNTER_CLOCKWISE_CIRCLE) == false)
                                    {
                                        cursorConfiguration.EnableGesture(PXCMCursorData.GestureType.CURSOR_COUNTER_CLOCKWISE_CIRCLE);
                                    }
                                    break;
                                case "cursor_hand_opening":
                                    if (cursorConfiguration.IsGestureEnabled(PXCMCursorData.GestureType.CURSOR_HAND_OPENING) == false)
                                    {
                                        cursorConfiguration.EnableGesture(PXCMCursorData.GestureType.CURSOR_HAND_OPENING);
                                    }
                                    break;
                                case "cursor_hand_closing":
                                    if (cursorConfiguration.IsGestureEnabled(PXCMCursorData.GestureType.CURSOR_HAND_CLOSING) == false)
                                    {
                                        cursorConfiguration.EnableGesture(PXCMCursorData.GestureType.CURSOR_HAND_CLOSING);
                                    }
                                    break;

                            }
                            cursorConfiguration.ApplyChanges();

                        }
                        else
                        {
                            cursorConfiguration.DisableAllGestures();
                            cursorConfiguration.ApplyChanges();
                        }
                    }
                    else
                    {
                        string gestureName = _form.GetGestureName();
                        if (handConfiguration != null)
                        {
                            if (string.IsNullOrEmpty(gestureName) == false)
                            {
                                if (handConfiguration.IsGestureEnabled(gestureName) == false)
                                {
                                    handConfiguration.DisableAllGestures();
                                    handConfiguration.EnableGesture(gestureName, true);
                                    handConfiguration.ApplyChanges();
                                }
                            }
                            else
                            {
                                handConfiguration.DisableAllGestures();
                                handConfiguration.ApplyChanges();
                            }
                        }
                    }

                    if (instance.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        break;
                    }

                    frameCounter++;

                    if (!DisplayDeviceConnection(!instance.IsConnected()))
                    {
                        PXCMCapture.Sample sample;
                        if (_form.GetCursorModeState())
                        {
                            sample = instance.QueryHandCursorSample();
                        }
                        else
                        {
                            sample = instance.QueryHandSample();
                        }
                        if (sample != null && sample.depth != null)
                        {
                            frameNumber = liveCamera ? frameCounter : instance.captureManager.QueryFrameIndex();

                            if (_form.GetCursorModeState())
                            {
                                if (cursorData != null)
                                {
                                    cursorData.Update();

                                    DisplayCursorPicture(sample.depth, cursorData);
                                    DisplayCursorGesture(cursorData, frameNumber);
                                    DisplayCursorJoints(cursorData);
                                    DisplayCursorAlerts(cursorData, frameNumber);
                                }
                            }
                            else
                            {
                                if (handData != null)
                                {
                                    handData.Update();

                                    DisplayPicture(sample.depth, handData);
                                    DisplayGesture(handData, frameNumber);
                                    DisplayJoints(handData);
                                    DisplayAlerts(handData, frameNumber);
                                }
                            }

                            _form.UpdatePanel();
                        }
                        timer.Tick();
                    }
                    instance.ReleaseFrame();
                }
            }
            else
            {
                _form.UpdateStatus("Init Failed");
                flag = false;
            }
            foreach (PXCMImage pxcmImage in _mImages)
            {
                pxcmImage.Dispose();
            }

            // Clean Up
            if (_form.GetCursorModeState())
            {
                if (handData != null) handData.Dispose();
                if (handConfiguration != null) handConfiguration.Dispose();
            }
            else
            {
                if (cursorData != null) cursorData.Dispose();
                if (cursorConfiguration != null) cursorConfiguration.Dispose();
            }
            instance.Close();
            instance.Dispose();

            if (flag)
            {
                _form.UpdateStatus("Stopped");
            }
        }
    }
}
