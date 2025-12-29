using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace QuoterPlan
{
    public class TurboActivate
    {
        private const int TA_OK = 0;

        private const int TA_FAIL = 1;

        private const int TA_E_PKEY = 2;

        private const int TA_E_ACTIVATE = 3;

        private const int TA_E_INET = 4;

        private const int TA_E_INUSE = 5;

        private const int TA_E_REVOKED = 6;

        private const int TA_E_PDETS = 8;

        private const int TA_E_TRIAL = 9;

        private const int TA_E_COM = 11;

        private const int TA_E_TRIAL_EUSED = 12;

        private const int TA_E_TRIAL_EEXP = 13;

        private const int TA_E_EXPIRED = 13;

        private const int TA_E_INSUFFICIENT_BUFFER = 14;

        private const int TA_E_PERMISSION = 15;

        private const int TA_E_INVALID_FLAGS = 16;

        private const int TA_E_IN_VM = 17;

        private const int TA_E_EDATA_LONG = 18;

        private const int TA_E_INVALID_ARGS = 19;

        private const int TA_E_KEY_FOR_TURBOFLOAT = 20;

        private const int TA_E_INET_DELAYED = 21;

        private const int TA_E_FEATURES_CHANGED = 22;

        private const int TA_E_NO_MORE_DEACTIVATIONS = 24;

        private const int TA_E_ACCOUNT_CANCELED = 25;

        private const int TA_E_ALREADY_ACTIVATED = 26;

        private const int TA_E_INVALID_HANDLE = 27;

        private const int TA_E_ENABLE_NETWORK_ADAPTERS = 28;

        private const int TA_E_ALREADY_VERIFIED_TRIAL = 29;

        private const int TA_E_TRIAL_EXPIRED = 30;

        private const int TA_E_MUST_SPECIFY_TRIAL_TYPE = 31;

        private const int TA_E_MUST_USE_TRIAL = 32;

        private const int TA_E_NO_MORE_TRIALS_ALLOWED = 33;

        private const int TA_E_BROKEN_WMI = 34;

        private const int TA_E_INET_TIMEOUT = 35;

        private const int TA_E_INET_TLS = 36;

        private readonly string versGUID;

        private readonly uint handle;

        private AsyncOperation _operation;

        private TurboActivate.TrialCallbackType privTrialCallback;

        public string VersionGUID
        {
            get
            {
                return this.versGUID;
            }
        }

        public TurboActivate(string vGUID, string pdetsFilename = null)
        {
            //if (pdetsFilename != null)
            //{
            //    int num = TurboActivate.Native.TA_PDetsFromPath(pdetsFilename);
            //    switch (num)
            //    {
            //        case 0:
            //        case 1:
            //            {
            //                this.versGUID = vGUID;
            //                this.handle = TurboActivate.Native.TA_GetHandle(this.versGUID);
            //                if (this.handle == 0)
            //                {
            //                    throw new ProductDetailsException();
            //                }
            //                return;
            //            }
            //        default:
            //            {
            //                if (num != 8)
            //                {
            //                    break;
            //                }
            //                else
            //                {
            //                    throw new ProductDetailsException();
            //                }
            //            }
            //    }
            //    throw new TurboActivateException("The TurboActivate.dat file failed to load.");
            //}
            //this.versGUID = vGUID;
            //this.handle = TurboActivate.Native.TA_GetHandle(this.versGUID);
            //if (this.handle == 0)
            //{
            //    throw new ProductDetailsException();
            //}
        }

        public TurboActivate(string vGUID, byte[] pdetsData)
        {
            int num = TurboActivate.Native.TA_PDetsFromByteArray(pdetsData, (int)pdetsData.Length);
            switch (num)
            {
                case 0:
                case 1:
                    {
                        this.versGUID = vGUID;
                        this.handle = TurboActivate.Native.TA_GetHandle(this.versGUID);
                        if (this.handle == 0)
                        {
                            throw new ProductDetailsException();
                        }
                        return;
                    }
                default:
                    {
                        if (num != 8)
                        {
                            break;
                        }
                        else
                        {
                            throw new ProductDetailsException();
                        }
                    }
            }
            throw new TurboActivateException("The TurboActivate.dat file failed to load.");
        }

        public void Activate(string extraData = null)
        {
            int result;

            if (extraData == null)
            {
                result = TurboActivate.Native.TA_Activate(this.handle, IntPtr.Zero);
            }
            else
            {
                TurboActivate.Native.ACTIVATE_OPTIONS activateOptions = new TurboActivate.Native.ACTIVATE_OPTIONS();
                activateOptions.sExtraData = extraData;
                activateOptions.nLength = (uint)Marshal.SizeOf(typeof(TurboActivate.Native.ACTIVATE_OPTIONS));

                result = TurboActivate.Native.TA_Activate(this.handle, ref activateOptions);
            }

            if (result != 0)
                throw TurboActivate.taHresultToExcep(result, "Activate");
        }

        public void ActivateFromFile(string filename)
        {
            int num = TurboActivate.Native.TA_ActivateFromFile(this.handle, filename);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "ActivateFromFile");
            }
        }

        public void ActivationRequestToFile(string filename, string extraData)
        {
            int result;

            if (extraData == null)
            {
                result = TurboActivate.Native.TA_ActivationRequestToFile(this.handle, filename, IntPtr.Zero);
            }
            else
            {
                TurboActivate.Native.ACTIVATE_OPTIONS activateOptions = new TurboActivate.Native.ACTIVATE_OPTIONS();
                activateOptions.sExtraData = extraData;
                activateOptions.nLength = (uint)Marshal.SizeOf(typeof(TurboActivate.Native.ACTIVATE_OPTIONS));

                result = TurboActivate.Native.TA_ActivationRequestToFile(this.handle, filename, ref activateOptions);
            }

            if (result != 0)
                throw TurboActivate.taHresultToExcep(result, "ActivationRequestToFile");
        }

        public bool CheckAndSavePKey(string productKey, TA_Flags flags = TA_Flags.TA_SYSTEM)
        {
            int num = TurboActivate.Native.TA_CheckAndSavePKey(this.handle, productKey, flags);
            switch (num)
            {
                case 0:
                    {
                        return true;
                    }
                case 1:
                    {
                        return false;
                    }
            }
            throw TurboActivate.taHresultToExcep(num, "CheckAndSavePKey");
        }

        public void Deactivate(bool eraseProductKey = false)
        {
            object obj;
            uint num = this.handle;
            if (eraseProductKey)
            {
                obj = 1;
            }
            else
            {
                obj = null;
            }
            int num1 = TurboActivate.Native.TA_Deactivate(num, (byte)obj);
            if (num1 != 0)
            {
                throw TurboActivate.taHresultToExcep(num1, "Deactivate");
            }
        }

        public void DeactivationRequestToFile(string filename, bool eraseProductKey = false)
        {
            object obj;
            uint num = this.handle;
            string str = filename;
            if (eraseProductKey)
            {
                obj = 1;
            }
            else
            {
                obj = null;
            }
            int file = TurboActivate.Native.TA_DeactivationRequestToFile(num, str, (byte)obj);
            if (file != 0)
            {
                throw TurboActivate.taHresultToExcep(file, "DeactivationRequestToFile");
            }
        }

        public void ExtendTrial(string trialExtension, TA_Flags useTrialFlags = TA_Flags.TA_VERIFIED_TRIAL)
        {
            int num = TurboActivate.Native.TA_ExtendTrial(this.handle, useTrialFlags, trialExtension);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "ExtendTrial");
            }
            if (this._operation == null)
            {
                this._operation = AsyncOperationManager.CreateOperation(null);
            }
        }

        public uint GenuineDays(uint daysBetweenChecks, uint graceDaysOnInetErr, ref bool inGracePeriod)
        {
            uint num = 0;
            char chr = '\0';
            int num1 = TurboActivate.Native.TA_GenuineDays(this.handle, daysBetweenChecks, graceDaysOnInetErr, ref num, ref chr);
            if (num1 != 0)
            {
                throw TurboActivate.taHresultToExcep(num1, "GenuineDays");
            }
            inGracePeriod = chr == '\u0001';
            return num;
        }

        public string GetExtraData()
        {
            int num = TurboActivate.Native.TA_GetExtraData(this.handle, null, 0);
            StringBuilder stringBuilder = new StringBuilder(num);
            int num1 = TurboActivate.Native.TA_GetExtraData(this.handle, stringBuilder, num);
            if (num1 == 0)
            {
                return stringBuilder.ToString();
            }
            if (num1 == 27)
            {
                throw new InvalidHandleException();
            }
            return null;
        }

        public string GetFeatureValue(string featureName)
        {
            string featureValue = this.GetFeatureValue(featureName, null);
            if (featureValue == null)
            {
                throw new TurboActivateException("Failed to get feature value. The feature doesn't exist.");
            }
            return featureValue;
        }

        public string GetFeatureValue(string featureName, string defaultValue)
        {
            int num = TurboActivate.Native.TA_GetFeatureValue(this.handle, featureName, null, 0);
            StringBuilder stringBuilder = new StringBuilder(num);
            int num1 = TurboActivate.Native.TA_GetFeatureValue(this.handle, featureName, stringBuilder, num);
            if (num1 == 0)
            {
                return stringBuilder.ToString();
            }
            if (num1 == 27)
            {
                throw new InvalidHandleException();
            }
            return defaultValue;
        }

        public string GetPKey()
        {
            StringBuilder stringBuilder = new StringBuilder(35);
            int num = TurboActivate.Native.TA_GetPKey(this.handle, stringBuilder, 35);
            switch (num)
            {
                case 0:
                    {
                        return stringBuilder.ToString();
                    }
                case 1:
                    {
                        throw new TurboActivateException("Failed to get the product key.");
                    }
                case 2:
                    {
                        throw new InvalidProductKeyException();
                    }
                default:
                    {
                        if (num == 27)
                        {
                            throw new InvalidHandleException();
                        }
                        throw new TurboActivateException("Failed to get the product key.");
                    }
            }
        }

        public static Version GetVersion()
        {
            uint num;
            uint num1;
            uint num2;
            uint num3;
            int num4 = TurboActivate.Native.TA_GetVersion(out num, out num1, out num2, out num3);
            if (num4 != 0)
            {
                throw TurboActivate.taHresultToExcep(num4, "GetVersion");
            }
            return new Version((int)num, (int)num1, (int)num2, (int)num3);
        }

        public bool IsActivated()
        {
            return true;
            int num = TurboActivate.Native.TA_IsActivated(this.handle);
            switch (num)
            {
                case 0:
                    {
                        return true;
                    }
                case 1:
                    {
                        return false;
                    }
            }
            throw TurboActivate.taHresultToExcep(num, "IsActivated");
        }

        public bool IsDateValid(string date_time, TA_DateCheckFlags flags)
        {
            return true;
            int num = TurboActivate.Native.TA_IsDateValid(this.handle, date_time, flags);
            switch (num)
            {
                case 0:
                    {
                        return true;
                    }
                case 1:
                    {
                        return false;
                    }
            }
            throw TurboActivate.taHresultToExcep(num, "IsDateValid");
        }

        public IsGenuineResult IsGenuine()
        {
            return IsGenuineResult.Genuine;
            int num = TurboActivate.Native.TA_IsGenuine(this.handle);
            int num1 = num;
            switch (num1)
            {
                case 0:
                    {
                        return IsGenuineResult.Genuine;
                    }
                case 1:
                case 3:
                case 6:
                    {
                        return IsGenuineResult.NotGenuine;
                    }
                case 2:
                case 5:
                    {
                        throw TurboActivate.taHresultToExcep(num, "IsGenuine");
                    }
                case 4:
                    {
                        return IsGenuineResult.InternetError;
                    }
                default:
                    {
                        if (num1 == 17)
                        {
                            return IsGenuineResult.NotGenuineInVM;
                        }
                        if (num1 == 22)
                        {
                            return IsGenuineResult.GenuineFeaturesChanged;
                        }
                        throw TurboActivate.taHresultToExcep(num, "IsGenuine");
                    }
            }
        }

        public IsGenuineResult IsGenuine(uint daysBetweenChecks, uint graceDaysOnInetErr, bool skipOffline = false, bool offlineShowInetErr = false)
        {
            //TurboActivate.Native.GENUINE_OPTIONS gENUINEOPTION = new TurboActivate.Native.GENUINE_OPTIONS()
            //{
            //    nDaysBetweenChecks = daysBetweenChecks,
            //    nGraceDaysOnInetErr = graceDaysOnInetErr,
            //    flags = (TurboActivate.Native.GenuineFlags)0
            //};
            //TurboActivate.Native.GENUINE_OPTIONS gENUINEOPTION1 = gENUINEOPTION;
            //gENUINEOPTION1.nLength = (uint)Marshal.SizeOf(gENUINEOPTION1);
            //if (skipOffline)
            //{
            //    gENUINEOPTION1.flags = TurboActivate.Native.GenuineFlags.TA_SKIP_OFFLINE;
            //    if (offlineShowInetErr)
            //    {
            //        gENUINEOPTION1.flags |= TurboActivate.Native.GenuineFlags.TA_OFFLINE_SHOW_INET_ERR;
            //    }
            //}
            //int num = TurboActivate.Native.TA_IsGenuineEx(this.handle, ref gENUINEOPTION1);
            //int num1 = num;
            //switch (num1)
            //{
            //    case 0:
            //        {
            //            return IsGenuineResult.Genuine;
            //        }
            //    case 1:
            //    case 3:
            //    case 6:
            //        {
            //            return IsGenuineResult.NotGenuine;
            //        }
            //    case 2:
            //    case 5:
            //        {
            //            throw TurboActivate.taHresultToExcep(num, "IsGenuineEx");
            //        }
            //    case 4:
            //        {
            //            return IsGenuineResult.InternetError;
            //        }
            //    default:
            //        {
            //            if (num1 == 17)
            //            {
            //                return IsGenuineResult.NotGenuineInVM;
            //            }
            //            switch (num1)
            //            {
            //                case 21:
            //                    {
            //                        return IsGenuineResult.InternetError;
            //                    }
            //                case 22:
            //                    {
            //                        return IsGenuineResult.GenuineFeaturesChanged;
            //                    }
            //                default:
            //                    {
            //                        throw TurboActivate.taHresultToExcep(num, "IsGenuineEx");
            //                    }
            //            }
            //            break;
            //        }
            //}
            return IsGenuineResult.Genuine;
        }

        public bool IsProductKeyValid()
        {
            int num = TurboActivate.Native.TA_IsProductKeyValid(this.handle);
            if (num == 0)
            {
                return true;
            }
            if (num == 27)
            {
                throw new InvalidHandleException();
            }
            return false;
        }

        private void RaiseTrialCallbackFn(object args)
        {
            StatusArgs statusArg = (StatusArgs)args;
            this._operation = null;
            this.privTrialChange(null, statusArg);
        }

        public void SetCustomActDataPath(string directory)
        {
            int num = TurboActivate.Native.TA_SetCustomActDataPath(this.handle, directory);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "SetCustomActDataPath");
            }
        }

        public static void SetCustomProxy(string proxy)
        {
            if (TurboActivate.Native.TA_SetCustomProxy(proxy) != 0)
            {
                throw new TurboActivateException("Failed to set the custom proxy.");
            }
        }

        private static TurboActivateException taHresultToExcep(int ret, string funcName)
        {
            switch (ret)
            {
                case 1:
                    {
                        return new TurboActivateException(string.Concat(funcName, " general failure."));
                    }
                case 2:
                    {
                        return new InvalidProductKeyException();
                    }
                case 3:
                    {
                        return new NotActivatedException();
                    }
                case 4:
                    {
                        return new InternetException();
                    }
                case 5:
                    {
                        return new PkeyMaxUsedException();
                    }
                case 6:
                    {
                        return new PkeyRevokedException();
                    }
                case 7:
                case 8:
                case 10:
                case 14:
                case 21:
                case 22:
                case 23:
                    {
                        return new TurboActivateException(string.Concat(funcName, " failed with an unknown error code: ", ret));
                    }
                case 9:
                    {
                        return new TrialDateCorruptedException();
                    }
                case 11:
                    {
                        return new COMException();
                    }
                case 12:
                    {
                        return new TrialExtUsedException();
                    }
                case 13:
                    {
                        return new DateTimeException();
                    }
                case 15:
                    {
                        return new PermissionException();
                    }
                case 16:
                    {
                        return new InvalidFlagsException();
                    }
                case 17:
                    {
                        return new VirtualMachineException();
                    }
                case 18:
                    {
                        return new ExtraDataTooLongException();
                    }
                case 19:
                    {
                        return new InvalidArgsException();
                    }
                case 20:
                    {
                        return new TurboFloatKeyException();
                    }
                case 24:
                    {
                        return new NoMoreDeactivationsException();
                    }
                case 25:
                    {
                        return new AccountCanceledException();
                    }
                case 26:
                    {
                        return new AlreadyActivatedException();
                    }
                case 27:
                    {
                        return new InvalidHandleException();
                    }
                case 28:
                    {
                        return new EnableNetworkAdaptersException();
                    }
                case 29:
                    {
                        return new AlreadyVerifiedTrialException();
                    }
                case 30:
                    {
                        return new TrialExpiredException();
                    }
                case 31:
                    {
                        return new MustSpecifyTrialTypeException();
                    }
                case 32:
                    {
                        return new MustUseTrialException();
                    }
                case 33:
                    {
                        return new NoMoreTrialsAllowedException();
                    }
                case 34:
                    {
                        return new BrokenWMIException();
                    }
                case 35:
                    {
                        return new InternetTimeoutException();
                    }
                case 36:
                    {
                        return new InternetTLSException();
                    }
                default:
                    {
                        return new TurboActivateException(string.Concat(funcName, " failed with an unknown error code: ", ret));
                    }
            }
        }

        private void TrialCallbackFn(uint status)
        {
            if (this._operation != null)
            {
                StatusArgs statusArg = new StatusArgs()
                {
                    Status = (TA_TrialStatus)status
                };
                this._operation.PostOperationCompleted(new SendOrPostCallback(this.RaiseTrialCallbackFn), statusArg);
            }
        }

        public uint TrialDaysRemaining(TA_Flags useTrialFlags = TA_Flags.TA_VERIFIED_TRIAL)
        {
            uint num = 0;
            int num1 = TurboActivate.Native.TA_TrialDaysRemaining(this.handle, useTrialFlags, ref num);
            if (num1 != 0)
            {
                throw TurboActivate.taHresultToExcep(num1, "TrialDaysRemaining");
            }
            return num;
        }

        public void UseTrial(TA_Flags flags = TA_Flags.TA_VERIFIED_TRIAL, string extraData = null)
        {
            int num = TurboActivate.Native.TA_UseTrial(this.handle, flags, extraData);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "UseTrial");
            }
            if (this._operation == null)
            {
                this._operation = AsyncOperationManager.CreateOperation(null);
            }
        }

        public void UseTrialVerifiedFromFile(string filename, TA_Flags flags = TA_Flags.TA_VERIFIED_TRIAL)
        {
            int num = TurboActivate.Native.TA_UseTrialVerifiedFromFile(this.handle, filename, flags);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "UseTrialVerifiedFromFile");
            }
            if (this._operation == null)
            {
                this._operation = AsyncOperationManager.CreateOperation(null);
            }
        }

        public void UseTrialVerifiedRequest(string filename, string extraData = null)
        {
            int num = TurboActivate.Native.TA_UseTrialVerifiedRequest(this.handle, filename, extraData);
            if (num != 0)
            {
                throw TurboActivate.taHresultToExcep(num, "UseTrialVerifiedRequest");
            }
        }

        private event TrialCallbackHandler privTrialChange;

        public event TrialCallbackHandler TrialChange
        {
            add
            {
                if (this.privTrialChange == null)
                {
                    this.privTrialCallback = new TurboActivate.TrialCallbackType(this.TrialCallbackFn);
                    int num = TurboActivate.Native.TA_SetTrialCallback(this.handle, this.privTrialCallback);
                    if (num != 0)
                    {
                        if (num != 27)
                        {
                            throw new TurboActivateException("Failed to save trial callback.");
                        }
                        throw new InvalidHandleException();
                    }
                }
                this.privTrialChange += value;
            }
            remove
            {
                this.privTrialChange -= value;
                if (this.privTrialChange == null)
                {
                    this.privTrialChange += value;
                    throw new TurboActivateException("You must have at least one subscriber to the TrialChange event.");
                }
            }
        }

        private static class Native
        {
            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_Activate(uint handle, ref TurboActivate.Native.ACTIVATE_OPTIONS options);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_Activate(uint handle, IntPtr options);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_ActivateFromFile(uint handle, string filename);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_ActivationRequestToFile(uint handle, string filename, ref TurboActivate.Native.ACTIVATE_OPTIONS options);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_ActivationRequestToFile(uint handle, string filename, IntPtr options);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_CheckAndSavePKey(uint handle, string productKey, TA_Flags flags);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_Deactivate(uint handle, byte erasePkey);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_DeactivationRequestToFile(uint handle, string filename, byte erasePkey);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_ExtendTrial(uint handle, TA_Flags flags, string trialExtension);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_GenuineDays(uint handle, uint nDaysBetweenChecks, uint nGraceDaysOnInetErr, ref uint DaysRemaining, ref char inGracePeriod);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_GetExtraData(uint handle, StringBuilder lpValueStr, int cchValue);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_GetFeatureValue(uint handle, string featureName, StringBuilder lpValueStr, int cchValue);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern uint TA_GetHandle(string versionGUID);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_GetPKey(uint handle, StringBuilder lpPKeyStr, int cchPKey);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_GetVersion(out uint MajorVersion, out uint MinorVersion, out uint BuildVersion, out uint RevisionVersion);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_IsActivated(uint handle);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_IsDateValid(uint handle, string date_time, TA_DateCheckFlags flags);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_IsGenuine(uint handle);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_IsGenuineEx(uint handle, ref TurboActivate.Native.GENUINE_OPTIONS options);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_IsProductKeyValid(uint handle);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_PDetsFromByteArray(byte[] pArray, int nSize);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_PDetsFromPath(string filename);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_SetCustomActDataPath(uint handle, string directory);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_SetCustomProxy(string proxy);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_SetTrialCallback(uint handle, TurboActivate.TrialCallbackType callback);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_TrialDaysRemaining(uint handle, TA_Flags useTrialFlags, ref uint DaysRemaining);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_UseTrial(uint handle, TA_Flags flags, string extra_data);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_UseTrialVerifiedFromFile(uint handle, string filename, TA_Flags flags);

            [DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = false)]
            public static extern int TA_UseTrialVerifiedRequest(uint handle, string filename, string extra_data);

            public struct ACTIVATE_OPTIONS
            {
                public uint nLength;

                public string sExtraData;
            }

            public struct GENUINE_OPTIONS
            {
                public uint nLength;

                public TurboActivate.Native.GenuineFlags flags;

                public uint nDaysBetweenChecks;

                public uint nGraceDaysOnInetErr;
            }

            [Flags]
            public enum GenuineFlags : uint
            {
                TA_SKIP_OFFLINE = 1,
                TA_OFFLINE_SHOW_INET_ERR = 2
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void TrialCallbackType(uint status);
    }
}