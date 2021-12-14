use std::{collections::HashMap, hash::Hash};

fn main() {
    let mut content = include_str!("../day14.in");
    let mut initial_seq: Vec<char> = content.lines().nth(0).unwrap().chars().collect();
    let mut transitions: HashMap<&str,char> = content.lines()
        .skip(2)
        .map(|line|line.split_once(" -> ").unwrap() )
        .map(|(from,to)|(from,to.chars().nth(0).unwrap()))
        .collect();

    // for (k,v) in transitions.iter() {
    //     println!("{} -> {}", k, v);
    // }
    let mut no_steps = 40;
    for i in 0..no_steps {
        let mut new_seq: Vec<char> = Vec::new();
        // let mut new_str = "";
        for p in initial_seq.windows(2) {
            let char_to_add = transitions.get(&p.iter().collect::<String>() as &str).unwrap();
            new_seq.push(p[0]);
            new_seq.push(*char_to_add);
        }
        new_seq.push(initial_seq[initial_seq.len()-1]);
        // for c in initial_seq.iter() {
        //     print!("{}", c);
        // }
        initial_seq.clear();
        initial_seq = new_seq.clone();
        // println!("i: {}| length: {}", i, initial_seq.len());
    }
    //get most common char
    let mut char_counts: HashMap<char,i128> = HashMap::new();
    for c in initial_seq.iter() {
        let count = char_counts.entry(*c).or_insert(0);
        *count += 1;
    }
    let mut most_count:i128 = 0;
    let mut lowest_count: i128 = 1_000_000;
    for (k,v) in char_counts.iter() {
        // println!("{} -> {}", k, v);
        if *v > most_count {
            most_count = *v;
        }
        if *v < lowest_count {
            lowest_count = *v;
        }
    }
    println!("part1: {}", most_count-lowest_count);
}
