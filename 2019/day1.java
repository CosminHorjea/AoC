import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.util.Scanner;

/**
 * day1
 */
public class day1 {

  public static void main(String[] args) {
    long neededFuel = 0;
    try {
      File in = new File("day1.in");
      Scanner myReader = new Scanner(in);
      while (myReader.hasNextInt()) {
        int currentData = myReader.nextInt();

        while (Math.floorDiv(currentData, 3) - 2 > 0) {
          neededFuel += Math.floorDiv(currentData, 3) - 2;
          currentData = Math.floorDiv(currentData, 3) - 2;
        }
      }
      myReader.close();
    } catch (FileNotFoundException e) {
      e.printStackTrace();
    }
    System.out.println(neededFuel);
  }
}