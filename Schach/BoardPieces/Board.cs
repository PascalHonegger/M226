﻿using Chess.BoardPieces.Cells;
using Chess.ChessPieces;

namespace Chess.BoardPieces
{
    public class Board
    {
        public int DimensionsChessBoard { get; } = 500;
        public Board()
        {
            CreateDefaultChessBoard();
        }

        private void CreateDefaultChessBoard()
        {
            A8 = new CellViewModel(new Rook(false), null, null, B8, B7, A7, null, null, null);
            B8 = new CellViewModel(new Knight(false), null, null, C8, C7, B7, A7, A8, null);
            C8 = new CellViewModel(new Bishop(false), null, null, D8, D7, C7, B7, B8, null);
            D8 = new CellViewModel(new King(false), null, null, E8, E7, D7, C7, C8, null);
            E8 = new CellViewModel(new Queen(false), null, null, F8, F7, E7, D7, D7, null);
            F8 = new CellViewModel(new Bishop(false), null, null, G8, G7, F7, E7, E8, null);
            G8 = new CellViewModel(new Knight(false), null, null, H8, H7, G7, F7, F8, null);
            H8 = new CellViewModel(new Rook(false), null, null, null, null, H7, G7, G8, null);

            A7 = new CellViewModel(new Pawn(false), A8, B8, B7, B6, A6, null, null, null);
            B7 = new CellViewModel(new Pawn(false), B8, C8, C7, C6, B6, A6, A7, A8);
            C7 = new CellViewModel(new Pawn(false), C8, D8, D7, D6, C6, B6, B7, B8);
            D7 = new CellViewModel(new Pawn(false), D8, E8, E7, E6, D6, C6, C7, C8);
            E7 = new CellViewModel(new Pawn(false), E8, F8, F7, F6, E6, D6, D6, D8);
            F7 = new CellViewModel(new Pawn(false), F8, G8, G7, G6, F6, E6, E7, E8);
            G7 = new CellViewModel(new Pawn(false), G8, H8, H7, H6, G6, F6, F7, F8);
            H7 = new CellViewModel(new Pawn(false), H8, null, null, null, H6, G6, G7, G8);

            A6 = new CellViewModel(null, A7, B7, B6, B5, A5, null, null, null);
            B6 = new CellViewModel(null, B7, C7, C6, C5, B5, A5, A6, A7);
            C6 = new CellViewModel(null, C7, D7, D6, D5, C5, B5, B6, B7);
            D6 = new CellViewModel(null, D7, E7, E6, E5, D5, C5, C6, C7);
            E6 = new CellViewModel(null, E7, F7, F6, F5, E5, D5, D6, D7);
            F6 = new CellViewModel(null, F7, G7, G6, G5, F5, E5, E6, E7);
            G6 = new CellViewModel(null, G7, H7, H6, H5, G5, F5, F6, F7);
            H6 = new CellViewModel(null, H7, null, null, null, H5, G5, G6, G7);

            A5 = new CellViewModel(null, A6, B6, B5, B4, A4, null, null, null);
            B5 = new CellViewModel(null, B6, C6, C5, C4, B4, A4, A5, A6);
            C5 = new CellViewModel(null, C6, D6, D5, D4, C4, B4, B5, B6);
            D5 = new CellViewModel(null, D6, E6, E5, E4, D4, C4, C5, C6);
            E5 = new CellViewModel(null, E6, F6, F5, F4, E4, D4, D5, D6);
            F5 = new CellViewModel(null, F6, G6, G5, G4, F4, E4, E5, E6);
            G5 = new CellViewModel(null, G6, H6, H5, H4, G4, F4, F5, F6);
            H5 = new CellViewModel(null, H6, null, null, null, H4, G4, G5, G6);

            A4 = new CellViewModel(null, A5, B5, B4, B3, A3, null, null, null);
            B4 = new CellViewModel(null, B5, C5, C4, C3, B3, A3, A4, A5);
            C4 = new CellViewModel(null, C5, D5, D4, D3, C3, B3, B4, B5);
            D4 = new CellViewModel(null, D5, E5, E4, E3, D3, C3, C4, C5);
            E4 = new CellViewModel(null, E5, F5, F4, F3, E3, D3, D4, D5);
            F4 = new CellViewModel(null, F5, G5, G4, G3, F3, E3, E4, E5);
            G4 = new CellViewModel(null, G5, H5, H4, H3, G3, F3, F4, F5);
            H4 = new CellViewModel(null, H5, null, null, null, H3, G3, G4, G5);

            A3 = new CellViewModel(null, A4, B4, B3, B2, A2, null, null, null);
            B3 = new CellViewModel(null, B4, C4, C3, C2, B2, A2, A3, A4);
            C3 = new CellViewModel(null, C4, D4, D3, D2, C2, B2, B3, B4);
            D3 = new CellViewModel(null, D4, E4, E3, E2, D2, C2, C3, C4);
            E3 = new CellViewModel(null, E4, F4, F3, F2, E2, D2, D3, D4);
            F3 = new CellViewModel(null, F4, G4, G3, G2, F2, E2, E3, E4);
            G3 = new CellViewModel(null, G4, H4, H3, H2, G2, F2, F3, F4);
            H3 = new CellViewModel(null, H4, null, null, null, H2, G2, G3, G4);

            A2 = new CellViewModel(new Pawn(true), A3, B3, B2, B1, A1, null, null, null);
            B2 = new CellViewModel(new Pawn(true), B3, C3, C2, C1, B1, A1, A2, A3);
            C2 = new CellViewModel(new Pawn(true), C3, D3, D2, D1, C1, B1, B2, B3);
            D2 = new CellViewModel(new Pawn(true), D3, E3, E2, E1, D1, C1, C2, C3);
            E2 = new CellViewModel(new Pawn(true), E3, F3, F2, F1, E1, D1, D2, D3);
            F2 = new CellViewModel(new Pawn(true), F3, G3, G2, G1, F1, E1, E2, E3);
            G2 = new CellViewModel(new Pawn(true), G3, H3, H2, H1, G1, F1, F2, F3);
            H2 = new CellViewModel(new Pawn(true), H3, null, null, null, H1, G1, G2, G3);

            A1 = new CellViewModel(new Rook(true), A2, B2, B1, null, null, null, null, null);
            B1 = new CellViewModel(new Knight(true), B2, C2, C1, null, null, null, A1, A2);
            C1 = new CellViewModel(new Bishop(true), C2, D2, D1, null, null, null, B1, B2);
            D1 = new CellViewModel(new King(true), D2, E2, E1, null, null, null, C1, C2);
            E1 = new CellViewModel(new Queen(true), E2, F2, F1, null, null, null, D2, D3);
            F1 = new CellViewModel(new Bishop(true), F2, G2, G1, null, null, null, E2, E3);
            G1 = new CellViewModel(new Knight(true), G2, H2, H1, null, null, null, F2, F3);
            H1 = new CellViewModel(new Rook(true), H2, null, null, null, null, null, G2, G3);
        }

        public CellViewModel A8 { get; set; }
        public CellViewModel B8 { get; set; }
        public CellViewModel C8 { get; set; }
        public CellViewModel D8 { get; set; }
        public CellViewModel E8 { get; set; }
        public CellViewModel F8 { get; set; }
        public CellViewModel G8 { get; set; }
        public CellViewModel H8 { get; set; }
        public CellViewModel A7 { get; set; }
        public CellViewModel B7 { get; set; }
        public CellViewModel C7 { get; set; }
        public CellViewModel D7 { get; set; }
        public CellViewModel E7 { get; set; }
        public CellViewModel F7 { get; set; }
        public CellViewModel G7 { get; set; }
        public CellViewModel H7 { get; set; }
        public CellViewModel A6 { get; set; }
        public CellViewModel B6 { get; set; }
        public CellViewModel C6 { get; set; }
        public CellViewModel D6 { get; set; }
        public CellViewModel E6 { get; set; }
        public CellViewModel F6 { get; set; }
        public CellViewModel G6 { get; set; }
        public CellViewModel H6 { get; set; }
        public CellViewModel A5 { get; set; }
        public CellViewModel B5 { get; set; }
        public CellViewModel C5 { get; set; }
        public CellViewModel D5 { get; set; }
        public CellViewModel E5 { get; set; }
        public CellViewModel F5 { get; set; }
        public CellViewModel G5 { get; set; }
        public CellViewModel H5 { get; set; }
        public CellViewModel A4 { get; set; }
        public CellViewModel B4 { get; set; }
        public CellViewModel C4 { get; set; }
        public CellViewModel D4 { get; set; }
        public CellViewModel E4 { get; set; }
        public CellViewModel F4 { get; set; }
        public CellViewModel G4 { get; set; }
        public CellViewModel H4 { get; set; }
        public CellViewModel A3 { get; set; }
        public CellViewModel B3 { get; set; }
        public CellViewModel C3 { get; set; }
        public CellViewModel D3 { get; set; }
        public CellViewModel E3 { get; set; }
        public CellViewModel F3 { get; set; }
        public CellViewModel G3 { get; set; }
        public CellViewModel H3 { get; set; }
        public CellViewModel A2 { get; set; }
        public CellViewModel B2 { get; set; }
        public CellViewModel C2 { get; set; }
        public CellViewModel D2 { get; set; }
        public CellViewModel E2 { get; set; }
        public CellViewModel F2 { get; set; }
        public CellViewModel G2 { get; set; }
        public CellViewModel H2 { get; set; }
        public CellViewModel A1 { get; set; }
        public CellViewModel B1 { get; set; }
        public CellViewModel C1 { get; set; }
        public CellViewModel D1 { get; set; }
        public CellViewModel E1 { get; set; }
        public CellViewModel F1 { get; set; }
        public CellViewModel G1 { get; set; }
        public CellViewModel H1 { get; set; }

        public void StartGame()
        {
            // TODO Enalble Click-Events
            // TODO Start White-Timer
        }

        public void ResetChessBoard()
        {
            CreateDefaultChessBoard();
        }
    }
}
