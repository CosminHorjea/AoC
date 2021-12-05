use std::cmp;
use std::collections::HashMap;
use std::fs;
use std::io::{self, Read};

struct Segment {
    start: (i32, i32),
    end: (i32, i32),
}
fn main() {
    //read from file day5.test
    let mut file = fs::read_to_string("day5.in").unwrap();
    // let mut file = fs::read_to_string("day5.test").unwrap();
    let mut segments: Vec<Segment> = file
        .lines()
        .map(|line| {
            let mut line = line.split(" -> ");
            let mut start = line.next().unwrap().split(",");
            let start = (
                start.next().unwrap().parse::<i32>().unwrap(),
                start.next().unwrap().parse::<i32>().unwrap(),
            );
            let mut end = line.next().unwrap().split(",");
            let end = (
                end.next().unwrap().parse::<i32>().unwrap(),
                end.next().unwrap().parse::<i32>().unwrap(),
            );
            Segment { start, end }
        })
        .collect::<Vec<Segment>>();

    let mut overlap: HashMap<(i32, i32), i32> = HashMap::new();
    // For Part 1
    // for seg in &segments {
    //     if (seg.start.0 != seg.end.0) && (seg.start.1 != seg.end.1) {
    //         continue;
    //     }
    //     if seg.start.0 != seg.end.0 {
    //         let y = seg.start.1;
    //         let mut x_min = cmp::min(seg.start.0, seg.end.0);
    //         let x_max = cmp::max(seg.start.0, seg.end.0);
    //         for x in x_min..x_max + 1 {
    //             let key = (x, y);
    //             if overlap.contains_key(&key) {
    //                 let val = overlap.get(&key).unwrap();
    //                 overlap.insert(key, *val + 1);
    //             } else {
    //                 overlap.insert(key, 1);
    //             }
    //         }
    //     } else {
    //         let x = seg.start.0;
    //         let mut y_min = cmp::min(seg.start.1, seg.end.1);
    //         let y_max = cmp::max(seg.start.1, seg.end.1);
    //         for y in y_min..y_max + 1 {
    //             let key = (x, y);
    //             if overlap.contains_key(&key) {
    //                 let val = overlap.get(&key).unwrap();
    //                 overlap.insert(key, *val + 1);
    //             } else {
    //                 overlap.insert(key, 1);
    //             }
    //         }
    //     }
    // }

    // print map
    // for (key, val) in &overlap {
    //     println!("{:?} : {:?}", key, val);
    // }
    // print!(
    //     "Part 1 {}, made possible with Rust",
    //     overlap.values().filter(|&(v)| v > &1).count()
    // );
    for seg in &segments {
        let mut x1 = seg.start.0;
        let mut x2 = seg.end.0;
        let mut y1 = seg.start.1;
        let mut y2 = seg.end.1;

        add_point_to_map(x1, y1, &mut overlap);
        while (x1 != x2) || (y1 != y2) {
            if x1 != x2 {
                if x1 < x2 {
                    x1 += 1;
                } else {
                    x1 -= 1;
                }
            }
            if y1 != y2 {
                if y1 < y2 {
                    y1 += 1;
                } else {
                    y1 -= 1;
                }
            }
            add_point_to_map(x1, y1, &mut overlap);
        }
    }
    println!(
        "Part 2: {}, made possible with Rust",
        overlap.values().filter(|&(v)| v > &1).count()
    );
}

fn add_point_to_map(x: i32, y: i32, overlap: &mut HashMap<(i32, i32), i32>) {
    let key = (x, y);
    if overlap.contains_key(&key) {
        let val = overlap.get(&key).unwrap();
        overlap.insert(key, *val + 1);
    } else {
        overlap.insert(key, 1);
    }
}
