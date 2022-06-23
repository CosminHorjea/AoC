import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Scanner;
import java.util.Set;

/**
 * day3
 */
public class day3 {
  @SuppressWarnings("unchecked")

  public static void main(String[] args) throws FileNotFoundException {
    File input = new File("day3.in");
    Set moves1 = new HashSet<pair<Character, Integer>>();
    Scanner sc = new Scanner(input);
    while (sc.hasNextLine()) {
      String data = sc.nextLine();
      String[] dataArr = data.split(",");
      for (String s : dataArr) {
        // System.out.println(s);
        moves1.add(new pair<Character, Integer>(s.charAt(0), 2));

      }
    }
    Iterator<pair<Character, Integer>> it = moves1.iterator();
    while (it.hasNext()) {
      System.out.println(it.next().First);
    }
  }

  public static class pair<first, second> {
    public first First;
    public second Second;

    private pair(first First, second Second) {
      this.First = First;
      this.Second = Second;
    }
  }

}