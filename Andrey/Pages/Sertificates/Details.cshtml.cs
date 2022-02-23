#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andrey.Data;
using Andrey.Models;
using IronPdf;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace Andrey.Pages.Sertificates
{
    public class DetailsModel : PageModel
    {
        private readonly Andrey.Data.AndreyContext _context;

        public DetailsModel(Andrey.Data.AndreyContext context)
        {
            _context = context;
        }

        public Sertificate Sertificate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sertificate = await _context.Sertificate
                .Include(s => s.Course)
                .Include(s => s.Student).FirstOrDefaultAsync(m => m.SertificateID == id);
            Sertificate.Student = _context.Student.FirstOrDefault(s => s.StudentID == Sertificate.StudentID);
            Sertificate.Student.User = _context.User.FirstOrDefault(s => s.UserID == Sertificate.Student.UserID);

            if (Sertificate == null)
            {
                return NotFound();
            }

            //string myUrl = this.Request.Scheme + "://" +  this.Request.Host + this.Request.Path + this.Request.QueryString;
            //MakePdf(myUrl);
            var h = GetMyHtml();
            byte[] barr = MakePdfFromHtml(h);
            return Page();
            //return File(barr, "application/pdf", "МОЙ СЕРТИФИКАТ.pdf");
        }
        //public void MakePdfFromUrl(string url)
        //{
        //    IronPdf.ChromePdfRenderer Renderer = new IronPdf.ChromePdfRenderer();
        //    var Pdf = Renderer.RenderUrlAsPdf(url);

        //    Pdf.SaveAs("url.pdf");
        //}
        public byte[] MakePdfFromHtml(string html)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            //Renderer.RenderHtmlAsPdf(html).SaveAs("pixel-perfect.pdf");
            return Renderer.RenderHtmlAsPdf(html).BinaryData;
        }

        public string GetMyHtml()
        {
            return Utils.PageExtensions.RenderViewAsync(this, "Details").ToString();

        }
    }
}

namespace Utils
{
    public static class PageExtensions
    {
        public static string RenderViewAsync(this PageModel pageModel, string pageName)
        {
            var actionContext = new ActionContext(
                pageModel.HttpContext,
                pageModel.RouteData,
                pageModel.PageContext.ActionDescriptor
            );

            using (var sw = new StringWriter())
            {
                IRazorViewEngine _razorViewEngine = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
                IRazorPageActivator _activator = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorPageActivator)) as IRazorPageActivator;

                var result = _razorViewEngine.FindPage(actionContext, pageName);

                if (result.Page == null)
                {
                    throw new ArgumentNullException($"The page {pageName} cannot be found.");
                }

                var page = result.Page;

                var view = new RazorView(_razorViewEngine,
                    _activator,
                    new List<IRazorPage>(),
                    page,
                    HtmlEncoder.Default,
                    new DiagnosticListener("ViewRenderService"));


                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    pageModel.ViewData,
                    pageModel.TempData,
                    sw,
                    new HtmlHelperOptions()
                );


                var pageNormal = ((Page)result.Page);

                pageNormal.PageContext = pageModel.PageContext;

                pageNormal.ViewContext = viewContext;


                _activator.Activate(pageNormal, viewContext);

                page.ExecuteAsync();

                return sw.ToString();
            }
        }
    }
}