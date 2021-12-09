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
    let mut total_risk = 0;
    for i in 0..map.len() {
        for j in 0..map[i].len() {
            total_risk += is_low_point(&map, i as i32, j as i32);
        }
    }
    // println!("{:?}", map);
    println!("Part1 {}, powered by Rustlang:tm:", total_risk);
}
