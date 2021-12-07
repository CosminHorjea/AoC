use std::fs;

fn main() {
    let input_file = "day2.in";
    let content =  fs::read_to_string(input_file).expect("Something went wrong reading the file");
    let mut total_distance = 0;
    let mut total_depth = 0;
    let mut aim = 0;
    for line in content.lines(){
        let command : &str = line.split_once(" ").unwrap().0;
        let distance : &str = line.split_once(" ").unwrap().1;
        match command {
            "forward" => {
                total_distance += distance.parse::<i32>().unwrap();
                total_depth += aim * distance.parse::<i32>().unwrap();
            },
            "down" => {
                aim += distance.parse::<i32>().unwrap();
            },
            "up" => {
                aim -= distance.parse::<i32>().unwrap();
            },
            _ => {
                println!("Error: {}",command);
            }
        }
    }
    print!("Part 1 : {}",total_depth*total_distance);

}
