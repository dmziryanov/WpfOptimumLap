using System.Collections.Generic;
using System.Linq;

namespace OptimumLap.Resources
{
    public partial class ParagraphStyleResources
    {
        public static readonly ParagraphStyleResources Default = new ParagraphStyleResources();

        private ParagraphStyleResources()
        {
            InitializeComponent();
        }

        public List<ParagraphStyle> Styles
        {
            get { return Values.OfType<ParagraphStyle>().OrderBy(p => p.SortOrder).ToList(); }
        }

        public ParagraphStyle Normal { get { return (ParagraphStyle)this["Normal"]; } }

        public ParagraphStyle Heading1 { get { return (ParagraphStyle)this["Heading1"]; } }
        public ParagraphStyle Heading2 { get { return (ParagraphStyle)this["Heading2"]; } }
        public ParagraphStyle Heading3 { get { return (ParagraphStyle)this["Heading3"]; } }
    }
}
