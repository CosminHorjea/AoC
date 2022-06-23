public class App {

    public static boolean satisfy(int i) {
        int aux = i;
        int repeat = 1;
        int repeatCond = 0;
        int c, prev = 10;
        while (aux != 0) {
            c = aux % 10;
            if (c > prev)
                return false;
            if (c == prev)
                repeat += 1;
            else {
                if (repeat == 2)
                    repeatCond = 1;
                repeat = 1;
            }
            prev = c;
            aux /= 10;
        }
        if (repeat == 2)
            repeatCond = 1;
        return (repeatCond == 1);
    }

    public static void main(String[] args) throws Exception {
        int number1 = 231832;
        int number2 = 767346;
        int count = 0;
        for (int i = number1; i <= number2; i++) {
            if (satisfy(i))
                count++;
        }
        System.out.println("count: " + count);
        // System.out.println(satisfy(112233));
        // System.out.println(satisfy(123444));
        // System.out.println(satisfy(111122));
        System.out.println(satisfy(112222));
    }
}
