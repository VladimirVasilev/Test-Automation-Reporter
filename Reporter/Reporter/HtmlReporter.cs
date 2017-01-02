namespace Reporter
{
    using System.IO;
    using System.Web.UI;
    
    public class HtmlReporter
    {
        private const string HtmlDocType = "<!DOCTYPE html>";
        private const string CssRelType = "stylesheet";
        private const string MainContainerId = "wrapper";
        private const string HeaderContainerId = "header";
        private const string TestCaseContainerClass = "test-case-container";
        private const string TestStepsContainerClass = "test-steps-container";
        private const string TestStepsSuccessClass = "success";
        private const string TestStepsFailureClass = "failure";

        public HtmlReporter()
        {
            this.StringWriter = new StringWriter();
            this.Writer = new HtmlTextWriter(this.StringWriter);
        }

        public StringWriter StringWriter { get; set; }

        public HtmlTextWriter Writer { get; set; }      

        public void InitiateHtmlReport(string pageTitle, string cssPath, string headerText)
        {
            this.InitiateHtmlDoctype();
            this.InitiateHead(pageTitle, cssPath);
            this.InitiateBodyAndMainContainer();
            this.InitiateHeader(headerText);
        }

        public void CompleteHtmlReportAndDispose()
        {
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();
            this.Writer.Dispose();
            this.StringWriter.Dispose();
        }

        public void StartTest(string testDescription, string methodName)
        {
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class, TestCaseContainerClass);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.Writer.RenderBeginTag(HtmlTextWriterTag.H1);
            this.Writer.Write(testDescription);
            this.Writer.RenderEndTag();

            this.Writer.RenderBeginTag(HtmlTextWriterTag.H2);
            this.Writer.Write(methodName);
            this.Writer.RenderEndTag();

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Class, TestStepsContainerClass);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Ol);
        }

        public void EndTest()
        {
            this.Writer.RenderEndTag();
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Hr);
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();
        }

        public void StartStep(string stepDescription)
        {
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Li);
            this.Writer.Write(stepDescription);            
        }

        public void EndStep(bool condition)
        {
            if (condition)
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class, TestStepsSuccessClass);
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                this.Writer.Write(" - Success!");
                this.Writer.RenderEndTag();
            }
            else
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Class, TestStepsFailureClass);
                this.Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                this.Writer.Write(" - Failure!");
                this.Writer.RenderEndTag();
            }

            this.Writer.RenderEndTag();
        }

        public void EndStep()
        {
            this.EndStep(true);
        }
        
        private void InitiateHtmlDoctype()
        {
            this.Writer.Write(HtmlDocType);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Html);
        }

        private void InitiateHead(string pageTitle, string cssFilePath)
        {
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Head);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Title);

            if (!string.IsNullOrEmpty(pageTitle))
            {                
                this.Writer.Write(pageTitle);
            }

            this.Writer.RenderEndTag();

            if (!string.IsNullOrEmpty(cssFilePath))
            {
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Href, cssFilePath);
                this.Writer.AddAttribute(HtmlTextWriterAttribute.Rel, CssRelType);                
            }

            this.Writer.RenderBeginTag(HtmlTextWriterTag.Link);
            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();
        }

        private void InitiateBodyAndMainContainer()
        {
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Body);

            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id, MainContainerId);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        private void InitiateHeader(string headerText)
        {
            this.Writer.AddAttribute(HtmlTextWriterAttribute.Id, HeaderContainerId);
            this.Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.Writer.RenderBeginTag(HtmlTextWriterTag.H1);
            if (!string.IsNullOrEmpty(headerText))
            {                
                this.Writer.Write(headerText);                
            }

            this.Writer.RenderEndTag();
            this.Writer.RenderEndTag();
        }
    }
}
