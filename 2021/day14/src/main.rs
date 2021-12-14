use std::{collections::HashMap, hash::Hash};

fn main() {
    let content = include_str!("../day14.in");
    let initial_seq: &str = content.lines().nth(0).unwrap();
    let first_char = initial_seq.chars().nth(0).unwrap();
    let last_char = initial_seq.chars().nth(initial_seq.len()-1).unwrap();
    let transitions: HashMap<&str,char> = content.lines()
        .skip(2)
        .map(|line|line.split_once(" -> ").unwrap() )
        .map(|(from,to)|(from,to.chars().nth(0).unwrap()))
        .collect();
    let mut freq: HashMap<String,i128> = HashMap::new();
    for p in initial_seq.chars().zip(initial_seq.chars().skip(1)) {
        let key = format!("{}{}", p.0, p.1);
        let count = freq.entry(key).or_insert(0);
        *count += 1;
    }
    
    let mut no_steps = 40;
    for i in 0..no_steps {
        let mut next_map = freq.clone();
        for (key,val) in freq.iter_mut(){
            if(*val <= 0){
                continue
            }
            *next_map.get_mut(key).unwrap() -= *val;
            let mut new_key_first = String::new();
            let mut new_key_second = String::new();
            let char_to_add = transitions.get(&key[0..2]).unwrap();
            // println!("{} -> {}", key, char_to_add);
            new_key_first = format!("{}{}", key.chars().nth(0).unwrap(), char_to_add);
            new_key_second = format!("{}{}", char_to_add,key.chars().nth(1).unwrap());
            // println!("{} -> {},{}", key, new_key_first,new_key_second);
            let mut count1 = next_map.entry(new_key_first).or_insert(0);
            *count1 += *val;
            let mut count2 = next_map.entry(new_key_second).or_insert(0);
            *count2 += *val;
        }
        println!("DOne iter {} : ----------", i+1);
        freq = next_map;
    }
    // for (k,v) in freq.iter() {
    //     println!("{} -> {}", k, v);
    // }

    let mut chars_count: HashMap<char,i128> = HashMap::new();
    for (k,v) in freq.iter() {
        if *v <= 0 {
            continue;
        }
        let count = chars_count.entry(k.chars().nth(0).unwrap()).or_insert(0);
        *count += *v;
        let count2 = chars_count.entry(k.chars().nth(1).unwrap()).or_insert(0);
        *count2 += *v;
    }
    *chars_count.get_mut(&first_char).unwrap()+=1;
    *chars_count.get_mut(&last_char).unwrap()+=1;

    let most_apparances = chars_count.iter().max_by_key(|(_,v)| *v).unwrap().1;
    let lest_apparances = chars_count.iter().min_by_key(|(_,v)| *v).unwrap().1;
    
    // println!("Most: {}, Least: {}", most_apparances, lest_apparances);
    println!("part2: {}", (most_apparances-lest_apparances)/2);
}
