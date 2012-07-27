using Novacode;
using System.Drawing;

namespace WFE.Ooxml
{
    internal static class WordExtensions
    {
        internal static Paragraph DocumentDescription(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore + 1;
            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Black,
                Size = 10,
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentPlaceholders(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore + 1;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Red,
                Size = 10,
                Highlight = Highlight.yellow,
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentDependencies(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore + 1;

            paragraph.InsertText("Dependent activities: ", false, new Formatting
            {
                Bold = true,
                Size = 10,
            });

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Black,
                FontFamily = new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif),
                Size = 9,
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentWorkflow(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 1 : parent.IndentationBefore + 1;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.DarkBlue,
                Size = 20,
                Bold = true,
                Misc = Misc.shadow
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentRootActivity(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 2 : parent.IndentationBefore + 1;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Blue,
                Size = 18,
                Bold = false,
                Italic = true,
                Misc = Misc.emboss
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentMetadata(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.DarkGray,
                Size = 10,
                Spacing = 0.1
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentLeafActivity(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Green,
                Size = 12,
            });

            body.InsertParagraph();

            return paragraph;
        }

        internal static Paragraph DocumentCompositeActivity(this DocX body, string input, Paragraph parent = null)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Paragraph paragraph = parent ?? body.InsertParagraph();
            paragraph.IndentationBefore = parent == null ? 3 : parent.IndentationBefore + 1;

            paragraph.InsertText(input, false, new Formatting
            {
                FontColor = Color.Black,
                Size = 16,
                Bold = false,
                Italic = false,
                UnderlineStyle = UnderlineStyle.dash,
                UnderlineColor = Color.Gray
            });

            body.InsertParagraph();

            return paragraph;
        }
    }
}
