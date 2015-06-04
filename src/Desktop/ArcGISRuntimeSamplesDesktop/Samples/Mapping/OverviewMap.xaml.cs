﻿using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Controls;
using System.Linq;
using System.Windows.Controls;

namespace ArcGISRuntime.Samples.Desktop
{
	/// <summary>
	/// This sample demonstrates displaying an overview map to indicate the extent of the parent map.
	/// </summary>
	/// <title>Overview Map</title>
	/// <category>Mapping</category>
	public partial class OverviewMap : UserControl
	{
		/// <summary>Construct Overview Map sample control</summary>
		public OverviewMap()
		{
			InitializeComponent();
			MyMapView.ExtentChanged += MyMapView_ExtentChanged;
		}

		private async void MyMapView_ExtentChanged(object sender, System.EventArgs e)
		{
			var graphicsOverlay = overviewMap.GraphicsOverlays["overviewOverlay"];

			// Update overview map graphic
			Graphic g = graphicsOverlay.Graphics.FirstOrDefault();
			if (g == null) //first time
			{
				g = new Graphic();
				graphicsOverlay.Graphics.Add(g);
			}
            // Get current viewpoints extent from the MapView
            var currentViewpoint = MyMapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry);
            var viewpointExtent = currentViewpoint.TargetGeometry.Extent;
			g.Geometry = viewpointExtent;

			// Adjust overview map scale
			await overviewMap.SetViewAsync(viewpointExtent.GetCenter(), MyMapView.Scale * 15);
		}
	}
}
