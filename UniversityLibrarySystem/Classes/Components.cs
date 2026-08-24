using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public static class Components
    {

        public static string bookCard(long ID, string Title, string PicName, string Author, string Publisher, int Available, bool IsSpecial)
        {
            string html = "";
            html += @"                                <div class=""properties-card"">
                                    <div class=""properties-img"">
                                        <a href=""bookdetail-" + ID + "-" + Uri.EscapeDataString(Title) + @"""><img src=""../img/books/" + (PicName.Trim()!=null?PicName:"no-photo.png") + @""" alt=" + Title + @" loading=""lazy""></a>
                                    </div>
                                    <div class=""properties-caption properties-caption2"">
                                        <h3><a href=""bookdetail-" + ID + "-" + Uri.EscapeDataString(Title) + @""">" + Title + "</a></h3>";
            html += @"<p>نویسنده:<span class=""author"">" + Author + @"</span></p>
                                        <p>ناشر:<span class=""author"">" + Publisher + @"</span></p>
                                        <div class=""disable-select-text mb-2"">";
            //Special Book Badge
            html += @"                                        <span class=""p-1";
            if (!IsSpecial)
                html += " opa-0";
            html += @" custom-badge-success"">کتاب ویژه</span>";
            //UnAvailable Book
            html += @"                                        <span class=""p-1";
            if (Available != 0)
                html += " opa-0";
            html += @" text-danger"">ناموجود</span>";
            html += @"</div>
                                        
                                    </div>
                                </div>";
            return html;
        }

        public enum AlertStyle
        {
            primary,
            secondary,
            success,
            danger,
            warning,
            info,
            light,
            dark
        }
        public static string alert(string text, AlertStyle alertStyle)
        {
            return @"<div class='alert alert-"+alertStyle.ToString()+@" alert-dismissible fade show' role='alert'>
  " + text + @"
  <button type = 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
</div> ";
        }
}
}