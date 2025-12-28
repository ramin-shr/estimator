using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace QuoterPlan
{
	public class TurboActivate
	{
		public string VersionGUID
		{
			get
			{
				return this.versGUID;
			}
		}

		public TurboActivate(string vGUID, string pdetsFilename = null)
		{
			if (pdetsFilename != null)
			{
				int num = TurboActivate.Native.TA_PDetsFromPath(pdetsFilename);
				switch (num)
				{
				case 0:
				case 1:
					break;
				default:
					if (num == 8)
					{
						throw new ProductDetailsException();
					}
					throw new TurboActivateException("The TurboActivate.dat file failed to load.");
				}
			}
			this.versGUID = vGUID;
			this.handle = TurboActivate.Native.TA_GetHandle(this.versGUID);
			if (this.handle == 0U)
			{
				throw new ProductDetailsException();
			}
		}

		public TurboActivate(string vGUID, byte[] pdetsData)
		{
			int num = TurboActivate.Native.TA_PDetsFromByteArray(pdetsData, pdetsData.Length);
			switch (num)
			{
			case 0:
			case 1:
				this.versGUID = vGUID;
				this.handle = TurboActivate.Native.TA_GetHandle(this.versGUID);
				if (this.handle == 0U)
				{
					throw new ProductDetailsException();
				}
				return;
			default:
				if (num == 8)
				{
					throw new ProductDetailsException();
				}
				throw new TurboActivateException("The TurboActivate.dat file failed to load.");
			}
		}

		private event TrialCallbackHandler privTrialChange
		{
			add
			{
				TrialCallbackHandler trialCallbackHandler = this.privTrialChange;
				TrialCallbackHandler trialCallbackHandler2;
				do
				{
					trialCallbackHandler2 = trialCallbackHandler;
					TrialCallbackHandler value2 = (TrialCallbackHandler)Delegate.Combine(trialCallbackHandler2, value);
					trialCallbackHandler = Interlocked.CompareExchange<TrialCallbackHandler>(ref this.privTrialChange, value2, trialCallbackHandler2);
				}
				while (trialCallbackHandler != trialCallbackHandler2);
			}
			remove
			{
				TrialCallbackHandler trialCallbackHandler = this.privTrialChange;
				TrialCallbackHandler trialCallbackHandler2;
				do
				{
					trialCallbackHandler2 = trialCallbackHandler;
					TrialCallbackHandler value2 = (TrialCallbackHandler)Delegate.Remove(trialCallbackHandler2, value);
					trialCallbackHandler = Interlocked.CompareExchange<TrialCallbackHandler>(ref this.privTrialChange, value2, trialCallbackHandler2);
				}
				while (trialCallbackHandler != trialCallbackHandler2);
			}
		}

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
						if (num == 27)
						{
							throw new InvalidHandleException();
						}
						throw new TurboActivateException("Failed to save trial callback.");
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

		private static TurboActivateException taHresultToExcep(int ret, string funcName)
		{
			switch (ret)
			{
			case 1:
				return new TurboActivateException(funcName + " general failure.");
			case 2:
				return new InvalidProductKeyException();
			case 3:
				return new NotActivatedException();
			case 4:
				return new InternetException();
			case 5:
				return new PkeyMaxUsedException();
			case 6:
				return new PkeyRevokedException();
			case 9:
				return new TrialDateCorruptedException();
			case 11:
				return new COMException();
			case 12:
				return new TrialExtUsedException();
			case 13:
				return new DateTimeException();
			case 15:
				return new PermissionException();
			case 16:
				return new InvalidFlagsException();
			case 17:
				return new VirtualMachineException();
			case 18:
				return new ExtraDataTooLongException();
			case 19:
				return new InvalidArgsException();
			case 20:
				return new TurboFloatKeyException();
			case 24:
				return new NoMoreDeactivationsException();
			case 25:
				return new AccountCanceledException();
			case 26:
				return new AlreadyActivatedException();
			case 27:
				return new InvalidHandleException();
			case 28:
				return new EnableNetworkAdaptersException();
			case 29:
				return new AlreadyVerifiedTrialException();
			case 30:
				return new TrialExpiredException();
			case 31:
				return new MustSpecifyTrialTypeException();
			case 32:
				return new MustUseTrialException();
			case 33:
				return new NoMoreTrialsAllowedException();
			case 34:
				return new BrokenWMIException();
			case 35:
				return new InternetTimeoutException();
			case 36:
				return new InternetTLSException();
			}
			return new TurboActivateException(funcName + " failed with an unknown error code: " + ret);
		}

		private void TrialCallbackFn(uint status)
		{
			if (this._operation != null)
			{
				StatusArgs statusArgs = new StatusArgs
				{
					Status = (TA_TrialStatus)status
				};
				this._operation.PostOperationCompleted(new SendOrPostCallback(this.RaiseTrialCallbackFn), statusArgs);
			}
		}

		private void RaiseTrialCallbackFn(object args)
		{
			StatusArgs e = (StatusArgs)args;
			this._operation = null;
			this.privTrialChange(null, e);
		}

		public void Activate(string extraData = null)
		{
			int num;
			if (extraData != null)
			{
				TurboActivate.Native.ACTIVATE_OPTIONS activate_OPTIONS = new TurboActivate.Native.ACTIVATE_OPTIONS
				{
					sExtraData = extraData
				};
				activate_OPTIONS.nLength = (uint)Marshal.SizeOf(activate_OPTIONS);
				num = TurboActivate.Native.TA_Activate(this.handle, ref activate_OPTIONS);
			}
			else
			{
				num = TurboActivate.Native.TA_Activate(this.handle, IntPtr.Zero);
			}
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "Activate");
			}
		}

		public void ActivationRequestToFile(string filename, string extraData)
		{
			int num;
			if (extraData != null)
			{
				TurboActivate.Native.ACTIVATE_OPTIONS activate_OPTIONS = new TurboActivate.Native.ACTIVATE_OPTIONS
				{
					sExtraData = extraData
				};
				activate_OPTIONS.nLength = (uint)Marshal.SizeOf(activate_OPTIONS);
				num = TurboActivate.Native.TA_ActivationRequestToFile(this.handle, filename, ref activate_OPTIONS);
			}
			else
			{
				num = TurboActivate.Native.TA_ActivationRequestToFile(this.handle, filename, IntPtr.Zero);
			}
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "ActivationRequestToFile");
			}
		}

		public void ActivateFromFile(string filename)
		{
			int num = TurboActivate.Native.TA_ActivateFromFile(this.handle, filename);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "ActivateFromFile");
			}
		}

		public bool CheckAndSavePKey(string productKey, TA_Flags flags = TA_Flags.TA_SYSTEM)
		{
			int ret = TurboActivate.Native.TA_CheckAndSavePKey(this.handle, productKey, flags);
			switch (ret)
			{
			case 0:
				return true;
			case 1:
				return false;
			default:
				throw TurboActivate.taHresultToExcep(ret, "CheckAndSavePKey");
			}
		}

		public void Deactivate(bool eraseProductKey = false)
		{
			int num = TurboActivate.Native.TA_Deactivate(this.handle, eraseProductKey ? 1 : 0);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "Deactivate");
			}
		}

		public void DeactivationRequestToFile(string filename, bool eraseProductKey = false)
		{
			int num = TurboActivate.Native.TA_DeactivationRequestToFile(this.handle, filename, eraseProductKey ? 1 : 0);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "DeactivationRequestToFile");
			}
		}

		public string GetExtraData()
		{
			int num = TurboActivate.Native.TA_GetExtraData(this.handle, null, 0);
			StringBuilder stringBuilder = new StringBuilder(num);
			int num2 = TurboActivate.Native.TA_GetExtraData(this.handle, stringBuilder, num);
			if (num2 == 0)
			{
				return stringBuilder.ToString();
			}
			if (num2 == 27)
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
			int num2 = TurboActivate.Native.TA_GetFeatureValue(this.handle, featureName, stringBuilder, num);
			if (num2 == 0)
			{
				return stringBuilder.ToString();
			}
			if (num2 == 27)
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
				return stringBuilder.ToString();
			case 1:
				break;
			case 2:
				throw new InvalidProductKeyException();
			default:
				if (num == 27)
				{
					throw new InvalidHandleException();
				}
				break;
			}
			throw new TurboActivateException("Failed to get the product key.");
		}

		public bool IsActivated()
		{
			int ret = TurboActivate.Native.TA_IsActivated(this.handle);
			switch (ret)
			{
			case 0:
				return true;
			case 1:
				return false;
			default:
				throw TurboActivate.taHresultToExcep(ret, "IsActivated");
			}
		}

		public bool IsDateValid(string date_time, TA_DateCheckFlags flags)
		{
			int ret = TurboActivate.Native.TA_IsDateValid(this.handle, date_time, flags);
			switch (ret)
			{
			case 0:
				return true;
			case 1:
				return false;
			default:
				throw TurboActivate.taHresultToExcep(ret, "IsDateValid");
			}
		}

		public IsGenuineResult IsGenuine()
		{
			int num = TurboActivate.Native.TA_IsGenuine(this.handle);
			int num2 = num;
			switch (num2)
			{
			case 0:
				return IsGenuineResult.Genuine;
			case 1:
			case 3:
			case 6:
				return IsGenuineResult.NotGenuine;
			case 2:
			case 5:
				break;
			case 4:
				return IsGenuineResult.InternetError;
			default:
				if (num2 == 17)
				{
					return IsGenuineResult.NotGenuineInVM;
				}
				if (num2 == 22)
				{
					return IsGenuineResult.GenuineFeaturesChanged;
				}
				break;
			}
			throw TurboActivate.taHresultToExcep(num, "IsGenuine");
		}

		public IsGenuineResult IsGenuine(uint daysBetweenChecks, uint graceDaysOnInetErr, bool skipOffline = false, bool offlineShowInetErr = false)
		{
			TurboActivate.Native.GENUINE_OPTIONS genuine_OPTIONS = new TurboActivate.Native.GENUINE_OPTIONS
			{
				nDaysBetweenChecks = daysBetweenChecks,
				nGraceDaysOnInetErr = graceDaysOnInetErr,
				flags = (TurboActivate.Native.GenuineFlags)0U
			};
			genuine_OPTIONS.nLength = (uint)Marshal.SizeOf(genuine_OPTIONS);
			if (skipOffline)
			{
				genuine_OPTIONS.flags = TurboActivate.Native.GenuineFlags.TA_SKIP_OFFLINE;
				if (offlineShowInetErr)
				{
					genuine_OPTIONS.flags |= TurboActivate.Native.GenuineFlags.TA_OFFLINE_SHOW_INET_ERR;
				}
			}
			int num = TurboActivate.Native.TA_IsGenuineEx(this.handle, ref genuine_OPTIONS);
			int num2 = num;
			switch (num2)
			{
			case 0:
				return IsGenuineResult.Genuine;
			case 1:
			case 3:
			case 6:
				return IsGenuineResult.NotGenuine;
			case 2:
			case 5:
				goto IL_A6;
			case 4:
				break;
			default:
				if (num2 == 17)
				{
					return IsGenuineResult.NotGenuineInVM;
				}
				switch (num2)
				{
				case 21:
					break;
				case 22:
					return IsGenuineResult.GenuineFeaturesChanged;
				default:
					goto IL_A6;
				}
				break;
			}
			return IsGenuineResult.InternetError;
			IL_A6:
			throw TurboActivate.taHresultToExcep(num, "IsGenuineEx");
		}

		public uint GenuineDays(uint daysBetweenChecks, uint graceDaysOnInetErr, ref bool inGracePeriod)
		{
			uint result = 0U;
			char c = '\0';
			int num = TurboActivate.Native.TA_GenuineDays(this.handle, daysBetweenChecks, graceDaysOnInetErr, ref result, ref c);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "GenuineDays");
			}
			inGracePeriod = (c == '\u0001');
			return result;
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

		public static void SetCustomProxy(string proxy)
		{
			if (TurboActivate.Native.TA_SetCustomProxy(proxy) != 0)
			{
				throw new TurboActivateException("Failed to set the custom proxy.");
			}
		}

		public uint TrialDaysRemaining(TA_Flags useTrialFlags = TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL)
		{
			uint result = 0U;
			int num = TurboActivate.Native.TA_TrialDaysRemaining(this.handle, useTrialFlags, ref result);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "TrialDaysRemaining");
			}
			return result;
		}

		public void UseTrial(TA_Flags flags = TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL, string extraData = null)
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

		public void UseTrialVerifiedRequest(string filename, string extraData = null)
		{
			int num = TurboActivate.Native.TA_UseTrialVerifiedRequest(this.handle, filename, extraData);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "UseTrialVerifiedRequest");
			}
		}

		public void UseTrialVerifiedFromFile(string filename, TA_Flags flags = TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL)
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

		public void ExtendTrial(string trialExtension, TA_Flags useTrialFlags = TA_Flags.TA_SYSTEM | TA_Flags.TA_VERIFIED_TRIAL)
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

		public void SetCustomActDataPath(string directory)
		{
			int num = TurboActivate.Native.TA_SetCustomActDataPath(this.handle, directory);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "SetCustomActDataPath");
			}
		}

		public static Version GetVersion()
		{
			uint major;
			uint minor;
			uint build;
			uint revision;
			int num = TurboActivate.Native.TA_GetVersion(out major, out minor, out build, out revision);
			if (num != 0)
			{
				throw TurboActivate.taHresultToExcep(num, "GetVersion");
			}
			return new Version((int)major, (int)minor, (int)build, (int)revision);
		}

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

		private TrialCallbackHandler privTrialChange;

		private TurboActivate.TrialCallbackType privTrialCallback;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void TrialCallbackType(uint status);

		private static class Native
		{
			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern uint TA_GetHandle(string versionGUID);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_Activate(uint handle, ref TurboActivate.Native.ACTIVATE_OPTIONS options);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_Activate(uint handle, IntPtr options);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_ActivationRequestToFile(uint handle, string filename, ref TurboActivate.Native.ACTIVATE_OPTIONS options);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_ActivationRequestToFile(uint handle, string filename, IntPtr options);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_ActivateFromFile(uint handle, string filename);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_CheckAndSavePKey(uint handle, string productKey, TA_Flags flags);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_Deactivate(uint handle, byte erasePkey);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_DeactivationRequestToFile(uint handle, string filename, byte erasePkey);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_GetExtraData(uint handle, StringBuilder lpValueStr, int cchValue);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_GetFeatureValue(uint handle, string featureName, StringBuilder lpValueStr, int cchValue);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_GetPKey(uint handle, StringBuilder lpPKeyStr, int cchPKey);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_IsActivated(uint handle);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_IsDateValid(uint handle, string date_time, TA_DateCheckFlags flags);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_IsGenuine(uint handle);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_IsGenuineEx(uint handle, ref TurboActivate.Native.GENUINE_OPTIONS options);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_GenuineDays(uint handle, uint nDaysBetweenChecks, uint nGraceDaysOnInetErr, ref uint DaysRemaining, ref char inGracePeriod);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_IsProductKeyValid(uint handle);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_SetCustomProxy(string proxy);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_TrialDaysRemaining(uint handle, TA_Flags useTrialFlags, ref uint DaysRemaining);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_UseTrial(uint handle, TA_Flags flags, string extra_data);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_UseTrialVerifiedRequest(uint handle, string filename, string extra_data);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_UseTrialVerifiedFromFile(uint handle, string filename, TA_Flags flags);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_ExtendTrial(uint handle, TA_Flags flags, string trialExtension);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_PDetsFromPath(string filename);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_PDetsFromByteArray(byte[] pArray, int nSize);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_SetCustomActDataPath(uint handle, string directory);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_SetTrialCallback(uint handle, TurboActivate.TrialCallbackType callback);

			[DllImport("TurboActivate.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern int TA_GetVersion(out uint MajorVersion, out uint MinorVersion, out uint BuildVersion, out uint RevisionVersion);

			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct ACTIVATE_OPTIONS
			{
				public uint nLength;

				[MarshalAs(UnmanagedType.LPWStr)]
				public string sExtraData;
			}

			[Flags]
			public enum GenuineFlags : uint
			{
				TA_SKIP_OFFLINE = 1U,
				TA_OFFLINE_SHOW_INET_ERR = 2U
			}

			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct GENUINE_OPTIONS
			{
				public uint nLength;

				public TurboActivate.Native.GenuineFlags flags;

				public uint nDaysBetweenChecks;

				public uint nGraceDaysOnInetErr;
			}
		}
	}
}
