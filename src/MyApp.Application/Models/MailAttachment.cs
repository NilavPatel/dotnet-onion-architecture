using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace MyApp.Application.Models
{
    public class MailAttachment
    {
        #region Fields

        private readonly Stream _stream;
        private readonly string _filename;
        private readonly string _mediaType;

        #endregion

        #region Properties

        public Stream Data { get { return _stream; } }
        public string Filename { get { return _filename; } }
        public string MediaType { get { return _mediaType; } }
        public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }

        #endregion

        #region Constructors

        public MailAttachment(byte[] data, string filename)
        {
            this._stream = new MemoryStream(data);
            this._filename = filename;
            this._mediaType = MediaTypeNames.Application.Octet;
        }

        public MailAttachment(string data, string filename)
        {
            this._stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
            this._filename = filename;
            this._mediaType = MediaTypeNames.Text.Html;
        }

        public MailAttachment(Stream stream, string filename)
        {
            this._stream = stream;
            this._filename = filename;
            this._mediaType = MediaTypeNames.Text.Html;
        }

        #endregion
    }
}