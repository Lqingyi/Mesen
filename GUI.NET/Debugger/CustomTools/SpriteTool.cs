using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
blockSize = 8
charSize = 4 
*/
namespace Mesen.GUI.Debugger.CustomTools
{
	public class SpriteConstant
	{
		// 一个瓦片图宽高（像素）
		static int TileSize
		{
			get { return 8; }
		}
		// Frame水平/垂直方向最多有几个Tile
		static int FrameSize
		{
			get { return 4; }
		}
	}
	public struct TileData
	{
		public const int PixelSize = 8;
		public const int BytePerPixel = 4;

		public int X, Y, Address, Value;
		public bool HorizontalFlip, VerticalFlip;

		public int Index;
		
		public int GetAddress()
		{
			return Address;
		}

		// to discard
		public int GetValue()
		{
			return Value;
		}

		public bool IsSameAs(TileData tileData)
		{
			if(X != tileData.X ||
				Y != tileData.Y ||
				Address != tileData.Address ||
				Value != tileData.Value ||
				HorizontalFlip != tileData.HorizontalFlip ||
				VerticalFlip != tileData.VerticalFlip)
			{
				return false;
			}
			return true;
		}

		public void GetValueFromByte(byte[] spritePixel, int index)
		{
			int start = BytePerPixel * (index / 8) * 8 * 8 * 16 + BytePerPixel * (index % 8) * 8;
			int r, g, b, a;
			for(int row=0; row < PixelSize; ++row)
			{
				for(int col=0; col < PixelSize; ++col)
				{
					r = spritePixel[start + row * PixelSize * 8 * 4 + col * 4 + 0];
					g = spritePixel[start + row * PixelSize * 8 * 4 + col * 4 + 1];
					b = spritePixel[start + row * PixelSize * 8 * 4 + col * 4 + 2];
					a = spritePixel[start + row * PixelSize * 8 * 4 + col * 4 + 3];
				}
			}
		}
	}


	public struct FrameData
	{
		public List<TileData> AllTile;

		public void SetAllTile(List<TileData> allTile)
		{
			int minX = 9999;
			int minY = 9999;
			foreach(TileData tileData in allTile)
			{
				if (tileData.X < minX)
				{
					minX = tileData.X;
				}
				if (tileData.Y < minY)
				{
					minY = tileData.Y;
				}
			}
			AllTile.Clear();
			TileData tempTileData;
			for(int i = 0; i < allTile.Count; ++i)
			{
				tempTileData = allTile[i];
				tempTileData.X -= minX;
				tempTileData.Y -= minY;
				AllTile.Add(tempTileData);
			}
		}

		public bool IsSameAs(FrameData otherFrameData)
		{
			if(AllTile.Count != otherFrameData.AllTile.Count)
			{
				return false;
			}
			bool hasSameTile;
			foreach(TileData tileData in AllTile)
			{
				hasSameTile = false;
				foreach(TileData otherTileData in otherFrameData.AllTile)
				{
					if (tileData.IsSameAs(otherTileData))
					{
						hasSameTile = true;
						break;
					}
				}
				if(!hasSameTile)
				{
					return false;
				}
			}
			return true;
		}
	}


	public struct ActionData
	{
		public List<FrameData> AllFrame;
		public string Name;
		public string Role;
	}
}
