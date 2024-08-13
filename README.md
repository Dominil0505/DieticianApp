# DietitianApp

# Database Design
## Database tables
- Dietician(dietician_id, dietician_name,email,password,avatar_url,description,mobile,role)
- Patient(patient_id,patient_name,email,password,age,description,mobile,height,weight,gender,role)
- Menu(menu_id, menu_name,coment,created_at,updated_at,created_by,menu_start,menu_end) -> A menu_start jelenti hogy p�ld�ul 2024.08.13-an 14:kor kezd�dik az a menu, pl eb�d stb.. d�tumot akarok benne t�rolni majd a napt�rhoz
- Food(food_id, food_name, calorie, protein, fat, carbohydrate, acid, ingredients, allergens)
- Disease( disease_id, disease_name)
- Medicine(medicine_id, medicine_name)
- Allergy(allergy_id, allergy_name)


## Kapcsol�t�bl�k - Relation Tables
- menu_food(menu_id, food_id) -> egy menuhoz (pl: eb�dhez) tartozhat t�bb �tel, de egy �tel nem tartozhat ugyan ahoz a menuhoz (eb�dhez) sz�val ha j�l gondolom ez 1:N kapcsolat, a menu itt pl a reggeli, eb�d, vacsora
- menu_assignment(dietician_id, menu_id, patient_id) -> ez az�rt kell hogy az adott men�t amit hozz� rendelek egy p�cienshez azt l�ssa mind a dietetikus mint a p�ciens. Ebben nem vagyok biztos hogy kell
- Dietetician_patient(dietetician_id, patient_id) -> egy dietetikushoz tartozhat t�bb p�ciens, de egy p�ciens csak egy dietetikushoz tartozhat ez is 1:N kapcsolat
- patient_diseases(patient_id, disease_id) -> egy p�cienshez tartozhat t�bb betegs�g �s egy betegs�g tartozhat t�bb p�cienshez N:M
- patient_medicine(patient_id, medicine_id) -> egy p�cienshez tartozhat t�bb gy�gyszer amit szed �s egy gy�gyszer tartozhat t�bb p�cienshez N:M 
- patient_allergy(patient_id, allergy_id) -> egy p�cienshez tartozhat t�bb allergia �s egy allergia tartozhat t�bb p�cienshez