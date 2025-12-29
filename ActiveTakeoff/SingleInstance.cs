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
        private Mutex mutex;

        private bool ownsMutex;

        private Guid identifier = Guid.Empty;

        private bool disposed;

        public bool IsFirstInstance
        {
            get
            {
                return this.ownsMutex;
            }
        }

        public SingleInstance(Guid identifier)
        {
            this.identifier = identifier;
            this.mutex = new Mutex(true, identifier.ToString(), out this.ownsMutex);
        }

        private void CallOnArgumentsReceived(object state)
        {
            this.OnArgumentsReceived((string[])state);
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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SingleInstance()
        {
            this.Dispose(false);
        }

        public int InstancesCount()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine(string.Concat("currentProcess.ProcessName=", currentProcess.ProcessName));
            Process[] processes = Process.GetProcesses();
            int num = 0;
            Process[] processArray = processes;
            for (int i = 0; i < (int)processArray.Length; i++)
            {
                if (processArray[i].ProcessName == currentProcess.ProcessName)
                {
                    num++;
                }
            }
            return num;
        }

        private void ListenForArguments(object state)
        {
            try
            {
                try
                {
                    using (NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(this.identifier.ToString()))
                    {
                        using (StreamReader streamReader = new StreamReader(namedPipeServerStream))
                        {
                            namedPipeServerStream.WaitForConnection();
                            List<string> strs = new List<string>();
                            while (namedPipeServerStream.IsConnected)
                            {
                                strs.Add(streamReader.ReadLine());
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallOnArgumentsReceived), strs.ToArray());
                        }
                    }
                }
                catch (IOException oException)
                {
                }
            }
            finally
            {
                this.ListenForArguments(null);
            }
        }

        public void ListenForArgumentsFromSuccessiveInstances()
        {
            if (!this.IsFirstInstance)
            {
                throw new InvalidOperationException("This is not the first instance.");
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ListenForArguments));
        }

        private void OnArgumentsReceived(string[] arguments)
        {
            if (this.ArgumentsReceived != null)
            {
                this.ArgumentsReceived(this, new ArgumentsReceivedEventArgs()
                {
                    Args = arguments
                });
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
                        string[] strArrays = arguments;
                        for (int i = 0; i < (int)strArrays.Length; i++)
                        {
                            streamWriter.WriteLine(strArrays[i]);
                        }
                    }
                }
                return true;
            }
            catch (TimeoutException timeoutException)
            {
            }
            catch (IOException oException)
            {
            }
            return false;
        }

        public event EventHandler<ArgumentsReceivedEventArgs> ArgumentsReceived;
    }
}