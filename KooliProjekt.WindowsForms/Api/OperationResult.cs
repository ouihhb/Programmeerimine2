using System.Collections.Generic;
using System.Linq;

namespace KooliProjekt.WindowsForms.Api
{
    public class OperationResult
    {
        public IList<string> Errors { get; } = new List<string>();
        public IDictionary<string, string> PropertyErrors { get; } = new Dictionary<string, string>();

        public bool HasErrors => Errors.Count > 0 || PropertyErrors.Count > 0;

        public OperationResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public OperationResult AddPropertyError(string property, string error)
        {
            if (!PropertyErrors.ContainsKey(property))
            {
                PropertyErrors.Add(property, error);
            }

            return this;
        }

        public string GetErrorMessage()
        {
            var propertyErrors = PropertyErrors.Select(x => $"{x.Key}: {x.Value}");
            return string.Join("\r\n", Errors.Concat(propertyErrors));
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Value { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(T value)
        {
            Value = value;
        }

        public new OperationResult<T> AddError(string error)
        {
            base.AddError(error);
            return this;
        }

        public new OperationResult<T> AddPropertyError(string property, string error)
        {
            base.AddPropertyError(property, error);
            return this;
        }
    }
}
