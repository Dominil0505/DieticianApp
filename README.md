# DietitianApp
## Ötlet - Idea
- Az ötlet az volt hogy készíteni akarok egy szoftvert ahol egy dietetikus tudja kezelni az étrendeket, látja a pácienseit.
- A dietetikus tud készíteni adott menüket (reggeli, ebéd stb..) tud felvenni ételeket, hozzávalókat amiket az elején felkell venni de azután ami már felvan véve azt ki lehet választani egyböl számolja a kaloriát fehérjét stb...
- Kell egy adminisztátor aki felveszi a pácienseket és hozzárendeli a dietetikushoz  !!! 

# Database Design
## Adatbázis táblák - Database tables
- Dietician(dietician_id, dietician_name,email,password,avatar_url,description,mobile,role)
- Patient(patient_id,patient_name,email,password,age,description,mobile,height,weight,gender,role)
- Menu(menu_id, menu_name,coment,created_at,updated_at,created_by,menu_start,menu_end) -> A menu_start jelenti hogy például 2024.08.13-an 14:kor kezdödik az a menu, pl ebéd stb.. dátumot akarok benne tárolni majd a naptárhoz
- Food(food_id, food_name, calorie, protein, fat, carbohydrate, acid, ingredients, allergens)
- Disease( disease_id, disease_name)
- Medicine(medicine_id, medicine_name)
- Allergy(allergy_id, allergy_name)


## Kapcsolótáblák - Relation Tables
- dietetician_patient(dietetician_id, patient_id) -> egy dietetikushoz tartozhat több páciens, de egy páciens csak egy dietetikushoz tartozhat ez is 1:N kapcsolat
- menu_assignment(dietician_id, menu_id, patient_id) -> Dietetikus Páciens közötti kapcsolat hogy lássa a menüt. Egy menu csak egy pácienshez tartozhat és csak egy dietetikus hozhat létre egy adott menüt
### N:M kapcsolatok
- patient_diseases (patient_id, disease_id) -> egy pácienshez tartozhat több betegség és egy betegség tartozhat több pácienshez N:M
- patient_medicine (patient_id, medicine_id) -> egy pácienshez tartozhat több gyógyszer amit szed és egy gyógyszer tartozhat több pácienshez N:M 
- patient_allergy (patient_id, allergy_id) -> egy pácienshez tartozhat több allergia és egy allergia tartozhat több pácienshez N:M
- food_ingredients (food_id, ingredients_id) -> egy ételhez tartozhat több hozzávaló és egy hozzávaló tartozhat több ételhez N:M

## Relációk Megtervezése
Dietetician -> Patient: Egy dietetikusnak több páciense is lehet, de egy pácienshez csak egy dietetikus tartozhat. Ez egy egy-a-többhöz kapcsolat.
- Dietician_Id lesz az idegen kulcs a Patient táblában.

Patient -> Menu: Egy páciensnek több étrendje lehet, de egy étrend csak egy pácienshez tartozhat. Ez szintén egy egy-a-többhöz kapcsolat.
- Patient_Id lesz az idegen kulcs a Menu táblában.

Dietetician -> Menu: Egy dietetikus több étrendet is készíthet, de egy étrend csak egy dietetikushoz tartozhat. Ez egy másik egy-a-többhöz kapcsolat.
- Dietician_Id lesz az idegen kulcs a Menu táblában.