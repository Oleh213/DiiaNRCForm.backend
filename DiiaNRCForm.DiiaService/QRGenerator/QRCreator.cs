using DiiaNRCForm.Abstractions.Interfaces;
using ImageMagick;
using Net.Codecrete.QrCodeGenerator;

namespace DiiaNRCForm.DiiaService.QRGenerator;

public class QRCreator : IQRCreator
{
    
    public string GenerateQRCode(string text)
    {
        var qrCode = QrCode.EncodeText(text, QrCode.Ecc.Medium);

        var magickImage = qrCode.ToImage(scale: 10, border: 0, MagickColors.Black, MagickColors.White);

        return magickImage.ToBase64();
    }
}
