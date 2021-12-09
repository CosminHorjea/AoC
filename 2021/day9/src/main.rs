use std::collections::VecDeque;

fn is_low_point(map: &Vec<Vec<i32>>, i: i32, j: i32) -> i32 {
    let mut count = 0;
    let mut max_count = 4;
    let mut moves = vec![(1, 0), (-1, 0), (0, 1), (0, -1)];
    for move_chosen in moves {
        let mut i_new = i + move_chosen.0;
        let mut j_new = j + move_chosen.1;
        if (i_new < 0 || i_new >= map.len() as i32 || j_new < 0 || j_new >= map[0].len() as i32) {
            max_count -= 1;
        } else {
            if map[i_new as usize][j_new as usize] > map[i as usize][j as usize] {
                count += 1;
            }
        }
    }
    if count == max_count {
        // print!(
        //     "Low point: {} | x:{},y:{},count:{}\n",
        //     map[i as usize][j as usize], i, j, count
        // );
        return map[i as usize][j as usize] + 1;
    }
    // println!();
    return 0;
}

fn main() {
    let content = include_str!("../day9.in");
    let mut map: Vec<Vec<i32>> = content
        .lines()
        .map(|line| {
            line.trim()
                .split("")
                .filter_map(|c| c.parse::<i32>().ok())
                .collect::<Vec<i32>>()
        })
        .collect();
    // let mut total_risk = 0;
    // for i in 0..map.len() {
    //     for j in 0..map[i].len() {
    //         total_risk += is_low_point(&map, i as i32, j as i32);
    //     }
    // }
    // println!("{:?}", map);
    // println!("Part1 {}, powered by Rustlang:tm:", total_risk);
    
    let mut sizes: Vec<i32> = vec![];
    for i in 0..map.len() {
        for j in 0..map[i].len() {
            if map[i][j] == 9 {
                continue;
            }
            let mut queue = VecDeque::from([(i as i32, j as i32)]);
            let start_point = (i as i32, j as i32);
            let mut curr_size = 0;
            while queue.len() > 0 {
                let (i, j) = queue.pop_front().unwrap();
                if map[i as usize][j as usize] == 9 {
                    continue;
                }
                map[i as usize][j as usize] = 9;
                curr_size+=1;
                let  moves = vec![(1, 0), (-1, 0), (0, 1), (0, -1)];
                for move_chosen in moves {
                    let  i_new = i + move_chosen.0;
                    let  j_new = j + move_chosen.1;
                    if i_new < 0 || i_new >= map.len() as i32 || j_new < 0 || j_new >= map[0].len() as i32 {
                        // println!("{:?} {:?}", i_new, j_new);
                        continue;
                    }
                    if map[i_new as usize][j_new as usize] != 9 {
                        queue.push_back((i_new, j_new));
                    }
                }
            }
            sizes.push(curr_size);
            // print!("x:{},y:{} => {}\n ",start_point.0,start_point.1,curr_size);
        }
    }
    // println!("len: {}\n",map[0].len());
    sizes.sort();
    println!("Part2 {} powered by RustLand:tm:",sizes[sizes.len()-3..sizes.len()].iter().product::<i32>());
}

//1019494 for someone else LUL