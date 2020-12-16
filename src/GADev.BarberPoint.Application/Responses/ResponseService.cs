using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GADev.BarberPoint.Application.Responses
{
    public class ResponseService
    {
        private readonly IList<string> _messages = new List<string>();
        public object Data { get; private set; }
        public string MessageError { get; private set; }
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; }

        public ResponseService(object data)
        {
            this.Success = true;
            this.Data = data;
        }

        public ResponseService(string messageError = "Error", IList<string> errors = null)
        {
            this.Success = false;
            this.Data = null;
            this.MessageError = messageError;
            this.Errors = errors;
        }

        public ResponseService() {
            this.Errors = new ReadOnlyCollection<string>(_messages);
            this.MessageError = "Um erro inesperado ocorreu";
            this.Success = false;
        }

        public ResponseService AddError(string message)
        {
            _messages.Add(message);

            return this;
        }
    }
}