use std::fs;

fn main() {
    let content: Vec<usize> = fs::read_to_string("day6.in")
        .unwrap()
        .split(",")
        .map(|x| x.parse::<usize>().unwrap())
        .collect::<Vec<usize>>();
    let mut frequency_of_days: [u128; 9] = [0, 0, 0, 0, 0, 0, 0, 0, 0];
    for num in content {
        frequency_of_days[num] += 1;
    }
    for _day in 0..256 {
        let day_zero = frequency_of_days[0];
        for i in 0..8 {
            frequency_of_days[i] = frequency_of_days[i + 1];
        }
        frequency_of_days[8] = day_zero;
        frequency_of_days[6] += day_zero;
    }
    let sum = frequency_of_days.clone().iter().fold(0, |sum, i| sum + i);

    print!("\nPart 1: {}", sum);
}
