use std::collections::HashMap;

fn main() {
    let content = include_str!("../day15.test");
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
    for i in 0..rows* 5{
        if i % 3 == 0 {
            println!("-------------------------------")
        }
        for j in 0..cols*5 {
            if j % 5 == 0 {
                print!("|")
            }
            print!("{}", risks[i][j]);
        }
        println!("");
        
    }

    for i in 0..rows * 5 {
        for j in 0..cols * 5 {
            // println!("{} {}", i, j);
            if i == 0 && j == 0 {
                continue;
            }
            if i == 0 {
                let prev = risks[i][j - 1];
                risks[i][j] += prev
            } else if j == 0 {
                let prev = risks[i - 1][j];
                risks[i][j] += prev
            } else {
                let previ = risks[i - 1][j];
                let prevj = risks[i][j - 1];
                risks[i][j] += previ.min(prevj);
            }
        }
    }
    println!(
        "Part 2 {} by RustLang ",
        risks[risks.len() - 1][risks[0].len() - 1] - risks[0][0]
    );
}

// too high 2935
