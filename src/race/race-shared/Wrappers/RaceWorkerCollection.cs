using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Shared.Wrappers
{
    public delegate void RaceTickProxy(Func<Task> task);
    public delegate Task RaceDelayProxy(int time);

    public class RaceWorkerCollection
    {
        public HashSet<RaceWorker> Workers;
        private RaceTickProxy TickProxy;
        private RaceDelayProxy DelayProxy;

        public RaceWorkerCollection(RaceTickProxy tickProxy, RaceDelayProxy delayProxy)
        {
            Workers = new HashSet<RaceWorker>();
            TickProxy = tickProxy;
            DelayProxy = delayProxy;
        }

        public void PushWorker(RaceWorker worker)
        {
            Workers.Add(worker);
            InitializeWorker(worker);
        }

        private void InitializeWorker(RaceWorker worker)
        {
            worker.DelayProxy = (delayTime) => DelayProxy.Invoke(delayTime);
            TickProxy.Invoke(worker.OnRefreshState);
            TickProxy.Invoke(worker.OnGameLogicTick);
            TickProxy.Invoke(worker.OnGameRenderTick);

            TickProxy.Invoke(() =>
            {
                worker.ProcessGameTasks();
                return DelayProxy.Invoke(100);
            });
        }
    }
}
