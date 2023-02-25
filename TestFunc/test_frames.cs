using System;
using System.Collections.Generic;


namespace TestFrames{

	public struct TileData
	{
		public const int PixelSize = 8;
		public const int BytePerPixel = 4;

		public int X, Y, Address, Value;
		public bool HorizontalFlip, VerticalFlip;
		
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
			int start = (index / 8) * BytePerPixel * 8 * 8 * 16 + (index % 8) * BytePerPixel * 8 * 16;
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


/*
blockSize = 8
charSize = 4


def SortBlock():

	allBlockInfo = [
		(0, 160, False),
	]
	allCharBlockInfo = []

	count = len(allBlockInfo)
	for index, blockInfo in enumerate(allBlockInfo):
		if not CheckOneChar(allCharBlockInfo, blockInfo):
			SaveCharBlock(allCharBlockInfo)
			allCharBlockInfo = []
		allCharBlockInfo.append(blockInfo)
		if index == count - 1:
			SaveCharBlock(allCharBlockInfo)


def CheckOneChar(allCharBlockInfo, blockInfo):
	x, y = blockInfo[:2]
	maxDis = blockSize * (charSize - 1)
	for charBlockInfo in allCharBlockInfo:
		# 判定不等
		charX, charY = charBlockInfo[:2]
		disX = abs(x - charX)
		disY = abs(y - charY)
		# 最大距离
		if disX > maxDis or disY > maxDis:
			return False
		# 至少一个坐标需要和现存block保持blocksize的距离，不然无法连接上，或者是堆叠状态
		if disX % blockSize != 0 and disY % blockSize == 0:
			return False
	return True
*/
