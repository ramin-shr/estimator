using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace QuoterPlan
{
	public class SingleInstance : IDisposable
	{
		public int InstancesCount()
		{
			Process currentProcess = Process.GetCurrentProcess();
			Console.WriteLine("currentProcess.ProcessName=" + currentProcess.ProcessName);
			Process[] processes = Process.GetProcesses();
			int num = 0;
			foreach (Process process in processes)
			{
				if (process.ProcessName == currentProcess.ProcessName)
				{
					num++;
				}
			}
			return num;
		}

		public SingleInstance(Guid identifier)
		{
			this.identifier = identifier;
			this.mutex = new Mutex(true, identifier.ToString(), ref this.ownsMutex);
		}

		public bool IsFirstInstance
		{
			get
			{
				return this.ownsMutex;
			}
		}

		public bool PassArgumentsToFirstInstance(string[] arguments)
		{
			if (this.IsFirstInstance)
			{
				throw new InvalidOperationException("This is the first instance.");
			}
			try
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(this.identifier.ToString()))
				{
					using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
					{
						namedPipeClientStream.Connect(200);
						foreach (string value in arguments)
						{
							streamWriter.WriteLine(value);
						}
					}
				}
				return true;
			}
			catch (TimeoutException)
			{
			}
			catch (IOException)
			{
			}
			return false;
		}

		public void ListenForArgumentsFromSuccessiveInstances()
		{
			if (!this.IsFirstInstance)
			{
				throw new InvalidOperationException("This is not the first instance.");
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ListenForArguments));
		}

		private void ListenForArguments(object state)
		{
			try
			{
				using (NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(this.identifier.ToString()))
				{
					using (StreamReader streamReader = new StreamReader(namedPipeServerStream))
					{
						namedPipeServerStream.WaitForConnection();
						List<string> list = new List<string>();
						while (namedPipeServerStream.IsConnected)
						{
							list.Add(streamReader.ReadLine());
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallOnArgumentsReceived), list.ToArray());
					}
				}
			}
			catch (IOException)
			{
			}
			finally
			{
				this.ListenForArguments(null);
			}
		}

		private void CallOnArgumentsReceived(object state)
		{
			this.OnArgumentsReceived((string[])state);
		}

		public event EventHandler<ArgumentsReceivedEventArgs> ArgumentsReceived
		{
			add
			{
				EventHandler<ArgumentsReceivedEventArgs> eventHandler = this.ArgumentsReceived;
				EventHandler<ArgumentsReceivedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ArgumentsReceivedEventArgs> value2 = (EventHandler<ArgumentsReceivedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ArgumentsReceivedEventArgs>>(ref this.ArgumentsReceived, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ArgumentsReceivedEventArgs> eventHandler = this.ArgumentsReceived;
				EventHandler<ArgumentsReceivedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ArgumentsReceivedEventArgs> value2 = (EventHandler<ArgumentsReceivedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ArgumentsReceivedEventArgs>>(ref this.ArgumentsReceived, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		private void OnArgumentsReceived(string[] arguments)
		{
			if (this.ArgumentsReceived != null)
			{
				this.ArgumentsReceived(this, new ArgumentsReceivedEventArgs
				{
					Args = arguments
				});
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (this.mutex != null && this.ownsMutex)
					{
						this.mutex.ReleaseMutex();
						this.mutex = null;
					}
					this.disposed = true;
				}
			}
			catch
			{
				this.mutex = null;
				this.disposed = true;
			}
		}

		~SingleInstance()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private Mutex mutex;

		private bool ownsMutex;

		private Guid identifier = Guid.Empty;

		private EventHandler<ArgumentsReceivedEventArgs> ArgumentsReceived;

		private bool disposed;
	}
}
