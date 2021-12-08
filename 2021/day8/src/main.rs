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
    let content: Vec<(&str, &str)> = include_str!("../day8.test")
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
    for (signals, output) in content {
        let mut translation: HashMap<u32, &str> = HashMap::new();
        let mut rev_translation: HashMap<&str, u32> = HashMap::new();
        //build the ones we know
        for word in signals.split(" ") {
            let sorted_word = sort_word(word);
            match word.len() {
                2 => {
                    translation.insert(1, word);
                    // rev_translation.insert(&sorted_word, 1);
                }
                4 => {
                    translation.insert(4, word);
                    // rev_translation.insert(&sorted_word, 4);
                }
                3 => {
                    translation.insert(7, word);
                    // rev_translation.insert(&sorted_word, 7);
                }
                7 => {
                    translation.insert(8, word);
                    // rev_translation.insert(&sorted_word, 8);
                }
                _ => continue,
            }
        }
        //build the others
        for word in signals.split(" ") {
            let sorted_word = sort_word(word);
            match word.len() {
                5 => {
                    if (num_of_common_chars(word, translation.get(&7).unwrap()) == 3) {
                        //i've found 3
                        translation.insert(3, word);
                        // rev_translation.insert(&sorted_word, 3);
                    } else if (num_of_common_chars(word, translation.get(&1).unwrap()) == 1) {
                        //we have 2 or 5
                        if (num_of_common_chars(word, translation.get(&4).unwrap()) == 3) {
                            // its a 5
                            translation.insert(5, word);
                            // rev_translation.insert(&sorted_word, 5);
                        } else {
                            //its a 2
                            translation.insert(2, word);
                            // rev_translation.insert(&sorted_word, 2);
                        }
                    }
                }
                6 => {
                    // case of 6,9,0
                    if (num_of_common_chars(word, translation.get(&4).unwrap()) == 4) {
                        // i've found 9
                        translation.insert(9, word);
                        // rev_translation.insert(&sorted_word, 9);
                    } else {
                        // it's a 6 or 0
                        if (num_of_common_chars(word, translation.get(&1).unwrap()) == 2) {
                            //its 0
                            translation.insert(0, word);
                            // rev_translation.insert(&sorted_word, 0);
                        } else {
                            //its a 6
                            translation.insert(6, word);
                            // rev_translation.insert(&sorted_word, 6);
                        }
                    }
                }
                _ => continue,
            }
        }

        for (k, v) in translation.iter() {
            print!("{} {} \n", k, v);
        }
        let numbers: Vec<u32>;
        let num: i8 = output
            .split(" ")
            .map(|wrd| {
                print!("wrd: {}\n", wrd);
                for (k, v) in translation.iter() {
                    print!("{} {} \n", k, v);
                }
                for i in 0..9 {
                    print!("i: {}\n", i);
                    if ((num_of_common_chars(wrd, translation.get(&i).unwrap()) == wrd.len())
                        && (wrd.len() == translation.get(&i).unwrap().len()))
                    {
                        return i.to_string();
                    }
                }
                "".to_string()
            })
            .collect::<Vec<String>>()
            .join("")
            .parse::<i8>()
            .unwrap();
        // let num = output
        //     .split(" ")
        //     .map(|x| sort_word(x))
        //     .map(|x| {
        //         rev_translation
        //             .get::<str>(&x.to_string())
        //             .unwrap()
        //             .to_string()
        //     })
        //     .collect::<Vec<String>>()
        //     .join("")
        //     .parse::<u32>()
        //     .unwrap();
        // let s: String;
        // s.clone()
        // print!("{}", nume);
    }
}
