
namespace webnode.MapCustom
{
    using System.Windows.Forms;
    using GMap.NET.WindowsForms;
    using System.Drawing;
    using GMap.NET;
    using System.Globalization;
   /// <summary>
   /// custom map of GMapControl
   /// </summary>
   public class Map : GMapControl
   {
      public long ElapsedMilliseconds;
      public string MapName;
      readonly Font DebugFont = new Font(FontFamily.GenericMonospace, 25, FontStyle.Regular);
      PointLatLng GmapToGpsOffset = new PointLatLng(-0.002649654980715, -0.00480212229727);

      /// <summary>
      /// any custom drawing here
      /// </summary>
      /// <param name="drawingContext"></param>
      protected override void OnPaintEtc(System.Drawing.Graphics g)
      {
          base.OnPaintEtc(g);
          string name = MapName;
          PointLatLng p = this.Position;
          p.Offset(GmapToGpsOffset);
          //name += "(" + p.Lng.ToString("F05", CultureInfo.InvariantCulture) + "," + p.Lat.ToString("F05", CultureInfo.InvariantCulture) + ")";
          g.DrawString(name, DebugFont, Brushes.White, 23, 23);

      }
   }
}
