using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSC.Shared.Wrappers
{
    public class RaceWorker
    {
        public Func<int, Task> DelayProxy { protected get; set; }
        public Queue<Action> GameTasks;

        public RaceWorker()
        {
            GameTasks = new Queue<Action>();
        }

        public virtual Task OnRefreshState()    => Task.FromResult(0);
        public virtual Task OnGameLogicTick()   => Task.FromResult(0);
        public virtual Task OnGameRenderTick()  => Task.FromResult(0);

        public Task ProcessGameTasks()
        {
            while (GameTasks.Count > 0)
            {
                Action callback = GameTasks.Dequeue();
                callback?.Invoke();
            }

            return Task.FromResult(0);
        }

        protected Task Wait(int ms)
        {
            return DelayProxy.Invoke(ms);
        }

        protected void GameTask(Action callback)
        {
            GameTasks.Enqueue(callback);
        }
    }
}
