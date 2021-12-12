use std::collections::{HashMap, HashSet, VecDeque};

fn main() {
    let content = include_str!("day12.in");
    let mut graph: HashMap<&str, HashSet<&str>> = HashMap::new();
    for line in content.lines() {
        let mut parts = line.split("-");
        let key = parts.next().unwrap();
        let value = parts.next().unwrap();
        graph.entry(key).or_insert(HashSet::new()).insert(value);
        graph.entry(value).or_insert(HashSet::new()).insert(key);
    }
    let mut queue: VecDeque<Vec<&str>> = VecDeque::new();
    let mut path = Vec::new();
    let mut total_paths:i128 = 0;
    path.push("start");
    queue.push_back(path);
    /**
     * This helped a lot https://www.geeksforgeeks.org/print-paths-given-source-destination-using-bfs/
     */

    while(queue.len() > 0) {
        path = queue.pop_front().unwrap();
        let key = path.last().unwrap();
        if *key == "end" {
            // println!("{:?}", path);
            total_paths+=1;
            // already_visited = false;
        } else {
            for value in graph.get(key).unwrap() {
                //Part 2 is slow a f, like 5 min for result
                if !(path.iter().filter(|x| x.chars().nth(0).unwrap().is_lowercase()).collect::<HashSet<&&str>>().len()+1 <
                path.iter().filter(|x| x.chars().nth(0).unwrap().is_lowercase()).collect::<Vec<&&str>>().len() ) || !value.chars().nth(0).unwrap().is_lowercase() {
                    if(*value == "start"){
                        continue;
                    }
                    let mut new_path = path.clone();
                    new_path.push(*value);
                    queue.push_back(new_path);
                }
            }
        }
    }
    // println!("Part 1 {:?}", total_paths);
    println!("Part 2 {:?}", total_paths);
}
