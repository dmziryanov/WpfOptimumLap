using System.Collections.Generic;
using OptimumLap.Resources;

namespace OptimumLap
{
    public class ParagraphStyleGallery
    {
        public ParagraphStyleGallery()
        {
            Styles = ParagraphStyleResources.Default.Styles;
            QuickAccessStyles = new List<ParagraphStyle>
            {
                ParagraphStyleResources.Default.Heading1,
                ParagraphStyleResources.Default.Heading2,
                ParagraphStyleResources.Default.Heading3
            };
        }

        public List<ParagraphStyle> Styles { get; set; }
        public List<ParagraphStyle> QuickAccessStyles { get; set; }
    }
}
