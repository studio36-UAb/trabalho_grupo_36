using PdfSharp.Fonts;

namespace Studio36.ReportComponent
{
    public class Studio36FontResolver : IFontResolver
    {
        public const string RegularFontFaceName = "Studio36#NotoSansRegular";
        public const string BoldFontFaceName = "Studio36#NotoSansBold";
        public const string FontFamilyName = "Studio36NotoSans";

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (!familyName.Equals(FontFamilyName, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return new FontResolverInfo(isBold ? BoldFontFaceName : RegularFontFaceName);
        }

        public byte[] GetFont(string faceName)
        {
            return faceName switch
            {
                RegularFontFaceName => LoadFontBytes("NotoSans-Regular.ttf"),
                BoldFontFaceName => LoadFontBytes("NotoSans-Bold.ttf"),
                _ => throw new ArgumentException($"Unknown font face name: {faceName}", nameof(faceName))
            };
        }

        private static byte[] LoadFontBytes(string fileName)
        {
            string fontPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Fonts", fileName);
            return File.ReadAllBytes(fontPath);
        }
    }
}
