﻿using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Chess.ChessPieces
{
	public interface IChessPiece : ICloneable
	{
		List<Path.Path> PathList { get; }
		BitmapSource Texture { get; }
		bool DidMove { get; set; }
		bool IsBlack();
		bool IsWhite();
		IChessPiece CloneChessPiece();
	}
}