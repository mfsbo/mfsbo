---
title: "Electric Vehicle Charging: Future of Autonomous Travel"
description: ""
date: 2024-11-16T14:53:32.827Z
preview: ""
tags: []
categories: []
type: default
---

## My Experience with Electric Vehicles

Recently I heard an [episode of Syntax podcast](https://syntax.fm/show/846/talking-evs-range-anxiety-charging-and-tech) where Wes (Van and EV) and Scott (two EVs) talk about range anxiety, charging and tech in Electric Vehicles. I have driven Renault Zoe, Nissan leaf and Tesla Model 3 in UK and US so listening to their experience from US/Canada side was interesting.

I hear their pros and cons of going fully electric or hybrid, range anxiety, charging infrastructure and how it is different in US and Canada compared to Europe. I started driving fully electric back in 2018. At that time I looked at few factors when I purchased Renault Zoe:

- I only used to drive 10-15 miles a day
- I heard that EV chargers are being installed at home so I don't have to go to petrol station and car can charge at home
- I heard that EVs are cheaper to run compared to petrol/diesel cars
- I heard that EVs are better for environment. Clean air zone signs were around and few other hints that electric cars are cheaper to run around these places as compared to petrol/diesel cars.

I was sold on the idea of electric cars and I bought Renault Zoe. I had a home charger installed and that time it took me 2 months to get one. I learned a lot about charging infrastructure during those 2 months and subscribed to many EV charging apps and payment cards. I did not buy a new card it was a used Zoe but I was committed to test it practically as much as possible. I was driving around to work, shops, hospitals, gym and I was charging my car at various places where parking is easy and charging is free or very cheap. I was saving money but I could only travel 90 miles before I needed 2 hour of charge. I started looking for better range.

In 2020 I was in another Zoe with range of 190 miles and I was getting comfortable with planning trips with longer stops and charging stations. In 2022 I started driving Tesla Model 3 long range and within few weeks I understood the differences in Tesla Charging network and other providers. The price tag of Tesla was too high as compared to range benefits, at least for me. I also started to compare Tesla Software with other Makes. The software update experience, release process, feature flags etc.

From 2018 to 2024 I have driven solely on electric and done about 60,000 miles. This includes trip from Scotland to London, Manchester to Norway via France, Netherlands, Germany, Denmark, Sweden and then back. Most of these, outside Manchester trips, include charging plan as we go.

## Charging Infrastructure

Lets say that in 2024 charging at home is not a problem. I still say using a 7kwh charger is better than sending less than 1 kwh to car. Installation of chargers cost between 200-600 depending on equipment needed. For UK you can buy a charger that you can lock and unlock via app, see when it was used and for how much and have few lights to indicate charging at night etc. Tesla charger can open your charging port if you get it close to vehicle.

For public charging my experience with charging apps has not been good. They do help but you need multiple of them to get confirmation that charging point is working. They rely on public to report if a charger is not working. Tesla chargers were reliable and used to show in app how many are available but these days you can even see busy times graph, price and if it would get busy by the time you reach there. Most of these features are missing in non tesla networks however they are improving.bp

From Scotland to London I never had any issue of charging even after Tesla opened their network to non-tesla vehicles. I was able to charge throughout Europe on tesla chargers without any issue, there were always more than 1 charger available about 80% of the time. The chargers were never too far from the place but it felt good when parking was filled and tesla chargers were not so you can always find parking and charge your car.

Cost of charging has increased over time with all providers. At some stage I was thinking that if I charge at home at busy time my electric cost is same as me charging at supercharger within 60 min so its better to go there and charge quicker. It has always stayed 1/2 or less than petrol/diesel cost. I have never paid about 7p to 54p per KwH so a 50KwH. Some people measure it weekly spend or monthly so on average I was spending 20 pound on driving about 300-400 miles. This has now increased to £30. Manchester to Norway I had to use full charge (around £17-£20 at that time) 7 times one way so the charging cost of the whole trip was less than £250. With 14 approx full charges we did about 2800 miles in total when trip was 2200 miles excluding ferry (2 hours between sweden and denmark) and euro-tunnel where car is just parked.

## Experience of Charging in Europe, UK and US

I will start with non-tesla chargers where I had to check in few apps which charger has been reported as working or available. After few trips to it I knew which app has truth about it. Sometimes the charger is not connected to internet and it will let you charge for free (pre 2021) but speed was slow. In 2024 these have become fast (1 hour to get 300 miles). Some of these charging stalls come with app which tells you when charging start and how much time remaining. It was not consistent and realtime and had to refresh app few times to see the status.

With Tesla network the charging experience was really great. I had the card and my payment method added to app. You go and park at the charging station (They are marked with A,B,C,D as groups and then A1,A2 in group A and B1,B2,B3 in group B). You plug your car in and it starts charging. In app you can see the progress and once its finished it will notify you to go and move the car out of stall otherwise idle fee would be charged which in UK is £1/min and shows £1 for me in Europe as well. The information was very realtime and app had animation to show and if you have two cars it can show both by just swipe left and right. Recent updates to app has brought more about charging network in navigation planner on your phone and car.

Tesla Charger stalls are arranged in Groups. Groups share energy so if 2 cars are charging on A1 nd A2 but B1,B2,B3 are empty it will be beneficial to both A1 and A2 to move one car to any B2 stall so they both get fast charging. The app and car does show how many stalls are empty but do not show which numbers (tha API does have numbers available to show so they can in future) The car estimate charging based on max charging speed available so sometimes if its busy you will get slow speed.

Other charging networks including white label (offered in different stores or offices) are now offering better experience but not as much integrated with car as Tesla is.

## Future of Charging

I can extrapolate few steps of what I think current development in this space is going however it wont be as realistic. So I am going to go with what I feel would be best based on how much I know about this space and what else can be done.

- Car and Charger should be able to identify each other including charge speed, energy required and payment method. Currently tesla has it via app but same protocol should apply to all apps and cars. This should be a standard interface that any car with charging can speak to charger and any charger can identify if its space is occupied by a unidentified object or a known car.
- Software and Hardware interfaces for chargers should be standard and open source to allow navigational AI to make better informed decision based on time, location, cost.
- A self driving car in a charging area should be able to decide which charging location/network it can pay for and go and charge on. Ideally it comes to car park with information in hand that a suitable charger is available where cost is in range that owner has setup, charger is available and car park restrictions are known to the car. Once parked the charger can communicate in real time and let the car decide when to move out of stall (even if its because of another space available)
- A car should be able to connect to the charger without any human intervention. The interface may be different for humans and machines (currently we struggle to keep same interface) but given that machines can connect these days without human intervention these two machines should be able to connect.
- Any app that uses car charging data with location should be able to export that to the car account or to a known cloud infrastructure. For Businesses this is useful for audit trail but for personal use its good to share few of these data with family or friends. This should be opt-in and opt-out feature.
- Source of energy supply should also be broadcasted by the charger and car should be able to opt in for a charger where energy source is x vs y. Cost of charging can also be dynamic based on energy source. Multiple energy sources should be available by default including grid as minimum.
- When a driver or self driving car parks in a stall to charge the stall the group should be able to communicate with other groups in same location and tell them if its sharing energy or there is a better way to charge based on preference of speedy charge, cost or other stalls available.
- A car should be able to pay for early parking and reserve a slot similar to how they can carry on parking and keep the slot reserved after the charge has finished. A pre booking of a stall or changing its state to allow disable parking or parking with kids would make parking more efficient.
- Degradation of charging stall should be broadcasted to the car and cost of charging should be adjusted based on speed and rate of charge. This should be communicated to the car and car should be able to decide if it wants to charge at that stall or not.

Some of these may rely on road traffic network to share data or cars sharing data about location of destinations however this data is currently available and not a new thing. Roads would be safer with EVs using their energy to not only run the car but communicate important information about their charge intentions as well as able to understand charging situation at the destination. This allows data analysis, prediction and training algorithms to be more accurate and efficient.

## Conclusion

- EV Adoption and Experience: Electric vehicles have evolved significantly since 2018, with improvements in range, charging infrastructure, and user experience, making EVs a viable choice for both daily commutes and long-distance travel.

- Challenges in Public Charging: While Tesla's network offers seamless charging with integrated payment and status updates, other networks still face issues with app reliability, charger availability, and consistency in user experience.

- Cost Efficiency: EV charging remains more economical than petrol or diesel, despite rising electricity prices, making it a cost-effective solution for both short and long-distance travel.

- Future of Charging Technology: Advancements in standardization, AI-driven navigation, autonomous charging, and dynamic energy source preferences can revolutionize the EV charging ecosystem, improving efficiency and accessibility.

- Integration and Sustainability: As charging networks become more integrated and environmentally focused, EVs can contribute to a cleaner, more efficient transport system, benefiting both individuals and society.