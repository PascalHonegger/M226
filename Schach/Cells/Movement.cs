namespace Chess.Cells
{
	/// <summary>
	/// Class that holds the important Enum Direction
	/// </summary>
	public static class Movement
	{
		/// <summary>
		/// Enum used for the orientation insade the chessboard
		/// </summary>
		public enum Direction
		{
			Top,
			TopRight,
			Right,
			BottomRight,
			Bottom,
			BottomLeft,
			Left,
			TopLeft,
			Final
		}
	}
}