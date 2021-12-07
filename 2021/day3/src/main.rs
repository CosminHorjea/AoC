use std::fs;
use std::isize;

fn main() {
    let input_file = "day3.in";
    // let input_file = "day3.test";
    let bin_len = 12;
    let content =  fs::read_to_string(input_file).unwrap();
    let mut freq : [i32;12] = [0,0,0,0,0,0,0,0,0,0,0,0];
    // let mut freq : [i32;5] = [0,0,0,0,0];
    let mut rows =0;
    for line in content.lines() {
        for (i,x) in line.chars().enumerate(){
           match x{
            '1' => {
                freq[i] += 1;
            },
            _=>{}
           }
        }
        rows+=1;
        
    }
    let mut epsilon: String = "".to_owned();
    let mut gamma: String = "".to_owned();
    for i in freq{
        if i > rows/2{
            epsilon = epsilon+"1";
            gamma = gamma+"0";
        }else{
            epsilon = epsilon+"0";
            gamma = gamma+"1";
        }
    }
    // let a = isize::from_str_radix(&epsilon,2).unwrap();
    // let b = isize::from_str_radix(&gamma,2).unwrap();
    // print!("part 1 : {}",a*b);

    let mut copy_of_data = content.lines().collect::<Vec<&str>>();
    let mut oxygen = Vec::new();
    let mut idx = 0;
    while idx < bin_len {
        if copy_of_data.len() == 1 {
            break;
        }
        let freq_of_ones = copy_of_data.iter().map(|x| x.chars().nth(idx).unwrap()).filter(|x| x == &'1').count();
        // print!("Freq: {}\n",freq_of_ones);
        // let mut preffered_bit = epsilon.chars().nth(idx).unwrap();
        let  preffered_bit = if freq_of_ones >= copy_of_data.len()-freq_of_ones {'1'}else{'0'};
        // print!("Pref: {}\n",preffered_bit);
        for line in 0..copy_of_data.len(){
            let  char_to_check = copy_of_data[line].chars().nth(idx).unwrap();
            if char_to_check == preffered_bit{
                oxygen.push(copy_of_data[line]);
            }
        }
        idx+=1;
        copy_of_data = oxygen.clone();
        oxygen.clear();
    }
    let oxygen_level = isize::from_str_radix(&copy_of_data[0],2).unwrap();
    
    let mut copy_of_data = content.lines().collect::<Vec<&str>>();
    let mut carbon = vec![];
    let mut idx = 0;
    while idx < bin_len {
        if copy_of_data.len() == 1 {
            break;
        }
        let freq_of_ones = copy_of_data.iter().map(|x| x.chars().nth(idx).unwrap()).filter(|x| x == &'1').count();
        let  preffered_bit = if freq_of_ones >= copy_of_data.len()-freq_of_ones {'0'}else{'1'};
        for line in 0..copy_of_data.len(){
            let  char_to_check = copy_of_data[line].chars().nth(idx).unwrap();
            if char_to_check == preffered_bit{
                carbon.push(copy_of_data[line]);
            }
        }
        idx+=1;
        copy_of_data = carbon.clone();
        carbon.clear();
    }
    let carbon_level = isize::from_str_radix(&copy_of_data[0],2).unwrap();

    print!("part 2 : {}",oxygen_level*carbon_level);



}
