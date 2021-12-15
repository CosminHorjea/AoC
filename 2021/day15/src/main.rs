use std::cmp::Reverse;
use std::collections::BinaryHeap;
use std::collections::HashMap;
use std::collections::HashSet;

fn main() {
    let content = include_str!("../day15.in");
    let scan = content
        .lines()
        .map(|line| {
            line.chars()
                .map(|c| c.to_digit(10).unwrap())
                .collect::<Vec<u32>>()
        })
        .collect::<Vec<Vec<u32>>>();
    let rows = scan.len();
    let cols = scan[0].len();
    // let mut risks: HashMap<(usize, usize), u32> = HashMap::new();
    let mut risks: Vec<Vec<u32>> = vec![vec![0; cols * 5]; rows * 5];
    for i in 0..rows * 5 {
        for j in 0..cols * 5 {
            let mut val_to_insert = scan[i % rows][j % cols] + (i / rows + j / cols) as u32;
            val_to_insert = val_to_insert % 10 + val_to_insert / 10;
            risks[i][j] = if val_to_insert == 10 {
                1
            } else {
                val_to_insert
            }
        }
    }

    // println!("{}", risks.len());
    let mut queue = BinaryHeap::new();
    queue.push((Reverse(0), (0, 0)));
    let mut visited: HashSet<(usize, usize)> = HashSet::new();
    while let Some((Reverse(risk), (i, j))) = queue.pop() {
        // println!("{} {}", i, j);
        if i == rows * 5 - 1 && j == cols * 5 - 1 {
            println!("Part 2: {}", risk);
            break;
        }
        let moves = [(0, -1), (1, 0), (0, 1), (-1, 0)];
        if visited.contains(&(i, j)) {
            continue;
        }
        visited.insert((i, j));
        for (x, y) in moves {
            let new_i = i as isize + x;
            let new_j = j as isize + y;
            if new_i < 0
                || new_j < 0
                || new_i >= (rows * 5) as isize
                || new_j >= (cols * 5) as isize
            {
                continue;
            }
            let new_risk = risk + risks[new_i as usize][new_j as usize];
            queue.push((Reverse(new_risk), (new_i as usize, new_j as usize)));
        }
    }
    // println!(
    //     "Part 2 {} by RustLang ",
    //     risks[risks.len() - 1][risks[0].len() - 1] - risks[0][0]
    // );
}

// too high 2935
