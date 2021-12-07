use std::i32;

fn get_fuel(units:u32) -> u32 {
  return (units+1)*units/2;
}

fn main() {
    let numbers: Vec<i32> = include_str!("../day7.in").split(",").map(|x| x.parse::<i32>().unwrap()).collect::<Vec<i32>>();

  let max_val = numbers.iter().max().unwrap();

  let mut minimum = 1186527293; // get a random big num  
  for i in 0..*max_val{

    let mut total_fuel = 0;
    for num in numbers.iter() {    
      total_fuel += get_fuel((num-i).abs() as u32);
    }
    // print!("i={} : {}\n",i, total_fuel);
    minimum = std::cmp::min(total_fuel, minimum);
    // if minimum > total_fuel{
    //   minimum = total_fuel;
    //   print!("Found it at {} : {}\n",i,minimum)
    // }
  }
  println!("Part 2: {}", minimum);
    
}
