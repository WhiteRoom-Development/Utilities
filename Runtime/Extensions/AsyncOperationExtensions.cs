using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Utilities.Runtime
{
    public static class AsyncOperationExtensions
    {
        /// <summary>
        /// Extension method that converts an AsyncOperation into a UniTask.
        /// </summary>
        /// <param name="asyncOperation">The AsyncOperation to convert.</param>
        /// <returns>A UniTask that represents the completion of the AsyncOperation.</returns>
        public static UniTask AsUniTask(this AsyncOperation asyncOperation)
        {
            var tcs = new UniTaskCompletionSource<bool>();
            asyncOperation.completed += _ => tcs.TrySetResult(true);
            return tcs.Task;
        }
        
        /// <summary>
        /// Extension method that converts an AsyncOperation into a Task.
        /// </summary>
        /// <param name="asyncOperation">The AsyncOperation to convert.</param>
        /// <returns>A Task that represents the completion of the AsyncOperation.</returns>
        public static Task AsTask(this AsyncOperation asyncOperation)
        {
            var tcs = new TaskCompletionSource<bool>();
            asyncOperation.completed += _ => tcs.SetResult(true);
            return tcs.Task;
        }
    }
}