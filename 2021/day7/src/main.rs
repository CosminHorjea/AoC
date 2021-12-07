use std::i32;

fn get_fuel(units:u32) -> u32 {
  return (units+1)*units/2;
}

fn main() {
  let numbers: Vec<i32> = include_str!("../day7.in").split(",").map(|x| x.parse::<i32>().unwrap()).collect::<Vec<i32>>();

  //sort numbers
  let mut numbers = numbers.clone();
  numbers.sort();

  let median =  numbers[numbers.len()/2];
  let mut total_fuel = 0;
  for num in &numbers{
    total_fuel += (num-median).abs();
  }
  println!("Part 1: {}", total_fuel);
  
  let avg = numbers.iter().sum::<i32>()/numbers.len() as i32;
  let mut total_fuel = 0;  
  for num in &numbers{
    total_fuel += get_fuel((num-avg).abs() as u32);
  }
    
  println!("Part 2: {}", total_fuel);
  
  // Brute force works as well, input is really small  
}
