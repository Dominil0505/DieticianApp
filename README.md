# DietitianApp

# Database Design
## Database tables
- Dietician(dietician_id, dietician_name,email,password,avatar_url,description,mobile,role)
- Patient(patient_id,patient_name,email,password,age,description,mobile,height,weight,gender,role)
- Menu(menu_id, menu_name,coment,created_at,updated_at,created_by,menu_start,menu_end) -> A menu_start jelenti hogy például 2024.08.13-an 14:kor kezdödik az a menu, pl ebéd stb.. dátumot akarok benne tárolni majd a naptárhoz
- Food(food_id, food_name, calorie, protein, fat, carbohydrate, acid, ingredients, allergens)
- Disease( disease_id, disease_name)
- Medicine(medicine_id, medicine_name)
- Allergy(allergy_id, allergy_name)


## Kapcsolótáblák - Relation Tables
- menu_food(menu_id, food_id) -> egy menuhoz (pl: ebédhez) tartozhat több étel, de egy étel nem tartozhat ugyan ahoz a menuhoz (ebédhez) szóval ha jól gondolom ez 1:N kapcsolat, a menu itt pl a reggeli, ebéd, vacsora
- menu_assignment(dietician_id, menu_id, patient_id) -> ez azért kell hogy az adott menüt amit hozzá rendelek egy pácienshez azt lássa mind a dietetikus mint a páciens. Ebben nem vagyok biztos hogy kell
- Dietetician_patient(dietetician_id, patient_id) -> egy dietetikushoz tartozhat több páciens, de egy páciens csak egy dietetikushoz tartozhat ez is 1:N kapcsolat
- patient_diseases(patient_id, disease_id) -> egy pácienshez tartozhat több betegség és egy betegség tartozhat több pácienshez N:M
- patient_medicine(patient_id, medicine_id) -> egy pácienshez tartozhat több gyógyszer amit szed és egy gyógyszer tartozhat több pácienshez N:M 
- patient_allergy(patient_id, allergy_id) -> egy pácienshez tartozhat több allergia és egy allergia tartozhat több pácienshez