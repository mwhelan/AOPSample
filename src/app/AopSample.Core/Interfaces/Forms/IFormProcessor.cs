using AopSample.Core.Commands;

namespace AopSample.Core.Interfaces.Forms
{
    public interface IFormProcessor
    {
        ExecutionResult Process<TForm>(TForm form)
            where TForm : class;
    }
}
