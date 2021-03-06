﻿using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.Win32.SafeHandles;

namespace Chess
{
	/// <summary>
	/// Here all Extensions Methods are stored
	/// </summary>
	public static class NativeMethods
	{
		/// <summary>
		/// Transforms a Bitmap to a BitmapSource
		/// </summary>
		/// <param name="source">Bitmap to be transformed</param>
		/// <returns>BitmapSource equivalent to the Bitmap-Input</returns>
		public static BitmapSource ToBitmapSource(this Bitmap source)
		{
			using (var handle = new SafeHBitmapHandle(source))
			{
				return Imaging.CreateBitmapSourceFromHBitmap(handle.DangerousGetHandle(),
					IntPtr.Zero, Int32Rect.Empty,
					BitmapSizeOptions.FromEmptyOptions());
			}
		}

		[DllImport("gdi32")]
		private static extern int DeleteObject(IntPtr o);

		private sealed class SafeHBitmapHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			[SecurityCritical]
			public SafeHBitmapHandle(Bitmap bitmap)
				: base(true)
			{
				SetHandle(bitmap.GetHbitmap());
			}

			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			protected override bool ReleaseHandle()
			{
				return DeleteObject(handle) > 0;
			}
		}
	}
}