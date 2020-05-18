using System;

namespace Fivet.ZeroIce
{

    
    class TheSystemImpl : model.TheSystemDisp_ {
         public override long getDelay(long clientTime, Ice.Current current)
        {
            return DateTime.Now.Ticks - clientTime;
        }
    }

    class Program
    {
        
        static int Main(string[] args)
        {
            try
            {
                using(Ice.Communicator communicator = Ice.Util.initialize(ref args))
                {
                    var adapter =
                        communicator.createObjectAdapterWithEndpoints("TheAdapter", "default -p 8080 -z");
                    adapter.add(new TheSystemImpl(), Ice.Util.stringToIdentity("cl.ucn.disc.pdis.fivet.zeroice.model.TheSystem"));
                    adapter.activate();

                    Console.WriteLine("Waiting for connections ..");

                    communicator.waitForShutdown();

                    
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }
            return 0;
        }
    }




    
}
