# Timetable Data Notes

## Tags/Elements

- `<tt:timetable>` (Root Tag)
    - The root of the timetable payload
    - Doesn't include anything notable
- `<allocation>` (Main Element)
    - Allocations are the `lessons`/`sessions` that you have in your timetable
    - They have no attributes, are explicitly an entity for other data
- `<subject>`
    - At most will contain 2 children
        - `<code>`: the Subject code
        - `<description>`: the Subject description
