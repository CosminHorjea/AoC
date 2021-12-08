use std::collections::hash_map::HashMap;
use std::fs;
fn num_of_common_chars(a: &str, b: &str) -> usize {
    let mut count = 0;
    for chr in a.chars() {
        if b.contains(chr) {
            count += 1;
        }
    }
    count
}

//sorts a given word
fn sort_word(word: &str) -> String {
    let mut chars: Vec<char> = word.chars().collect();
    chars.sort();
    chars.into_iter().collect()
}

fn main() {
    let content: Vec<(&str, &str)> = include_str!("../day8.in")
        .lines()
        .map(|line| line.split_once(" | ").unwrap())
        .collect();
    let mut freq = [0; 10];
    // for (_id, text) in content {
    //     for word in text.split(" ") {
    //         freq[word.len()] += 1;
    //     }
    // }
    // print!("Part 1: {}", freq[2] + freq[4] + freq[3] + freq[7]);

    let mut total_sum:u32 = 0;
    for (signals, output) in content {
        let mut translation: HashMap<u32, &str> = HashMap::new();
        //build the ones we know
        for word in signals.split(" ") {
            match word.len() {
                2 => {
                    translation.insert(1, word);
                }
                4 => {
                    translation.insert(4, word);
                }
                3 => {
                    translation.insert(7, word);
                }
                7 => {
                    translation.insert(8, word);
                }
                _ => continue,
            }
        }
        //build the others
        for word in signals.split(" ") {
            match word.len() {
                5 => {
                    if (num_of_common_chars(word, translation.get(&7).unwrap()) == 3) {
                        //i've found 3
                        translation.insert(3, word);
                    } else if (num_of_common_chars(word, translation.get(&1).unwrap()) == 1) {
                        //we have 2 or 5
                        if (num_of_common_chars(word, translation.get(&4).unwrap()) == 3) {
                            // its a 5
                            translation.insert(5, word);
                        } else if (num_of_common_chars(word, translation.get(&4).unwrap()) == 2) {
                            //its a 2
                            translation.insert(2, word);
                        }
                    }
                }
                6 => {
                    // case of 6,9,0
                    if (num_of_common_chars(word, translation.get(&4).unwrap()) == 4) {
                        // i've found 9
                        translation.insert(9, word);
                    } else if (num_of_common_chars(word, translation.get(&4).unwrap()) == 3 ){
                        // it's a 6 or 0
                        if (num_of_common_chars(word, translation.get(&1).unwrap()) == 2) {
                            //its 0
                            translation.insert(0, word);
                        } else if (num_of_common_chars(word, translation.get(&1).unwrap()) == 1){
                            //its a 6
                            translation.insert(6, word);
                        }
                    }
                }
                _ => continue,
            }
        }


        let mut number :String = String::new();
        for number_as_string in output.split(" ") {
            for (k,v) in translation.iter() {
                if (num_of_common_chars(v, number_as_string) == v.len()) && (v.len() == number_as_string.len()) {
                    number.push_str(&k.to_string());
                }
            }
        }
        total_sum+=number.parse::<u32>().unwrap();
    }
    println!("part 2: {}", total_sum);
}
