package ro.ubb.lab8_good.model;

public class Board {
    public int[][] board;
    public int shipsAdded;

    public Board() {
        board = new int[6][6];
        shipsAdded = 0;
    }

    public int getForPosition(int x, int y) {
        return board[x][y];
    }

    public void addShip(int x, int y, int orientation) {
        int[] nextX = {-1, 0, 1, 0};
        int[] nextY = {0, -1, 0, 1}; //up, left, down, right
        int currentX = x;
        int currentY = y;

        for(int i = 0; i < 3; ++i) {
            if(currentX >=0 && currentX < 6 && currentY >=0 && currentY < 6 )
                board[currentX][currentY] = 1;
            currentX += nextX[orientation];
            currentY += nextY[orientation];
        }
        shipsAdded += 1;
    }

    public void attack(int x, int y) {
        board[x][y] += 2;
    }
}
