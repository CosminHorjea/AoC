use std::collections::{HashMap, HashSet};

fn main() {
    let content = include_str!("day12.test");
    let mut graph: HashMap<&str, HashSet<&str>> = HashMap::new();
    for line in content.lines() {
        let mut parts = line.split("-");
        let key = parts.next().unwrap();
        let value = parts.next().unwrap();
        graph.entry(key).or_insert(HashSet::new()).insert(value);
        graph.entry(value).or_insert(HashSet::new()).insert(key);
    }
    //print all paths from start to end
    let mut sttack: Vec<(&str)> = vec![("start")];
    let mut paths: Vec<Vec<&str>> = Vec::new();
    let mut visited: HashSet<&str> = HashSet::new();
    while let Some((key)) = sttack.pop() {
        visited.insert(key);
        if key == "end" {
            println!("{:?}", 1);
            // for path in paths.iter() {
            //     println!("{:?}", path);
            // }
            paths.push(sttack.clone().iter().rev().map(|x| *x).collect());
        } else {
            for value in graph.get(key).unwrap() {
                if !visited.contains(value) || !value.chars().nth(0).unwrap().is_lowercase() {
                    sttack.push((value));
                }
            }
        }
    }

    //print paths
    for path in paths.iter() {
        println!("{:?}", path);
    }
}
