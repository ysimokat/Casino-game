Module Module1

    Sub Main()
        Const MAX_SIZE As Integer = 50
        Dim names(MAX_SIZE) As String
        Dim wins(MAX_SIZE), losses(MAX_SIZE), skills(MAX_SIZE) As Integer
        Dim age As Integer
        Dim changeSkill As Integer
        Dim arr(MAX_SIZE) As String
        Dim menuChoice As Integer
        Dim numPlayers As Integer = loadPlayers(names, age, skills, wins, losses)
        Dim year As Integer
        Dim initialAge As Integer
        Dim storeAge As Integer
        Dim roster(50) As player

        ' menu
        Console.Write("   Menu ")
        Console.WriteLine("Year 2015")
        Console.WriteLine("=======================")
        Console.WriteLine("1) View Records")
        Console.WriteLine("2) Simulate Matches")
        Console.WriteLine("3) Add New Player")
        Console.WriteLine("4) Change Player's Rank")
        Console.WriteLine("5) Advance to Next Season (Players will Age One Year)")
        Console.WriteLine("6) Exit")
        Console.WriteLine("=======================")
        Console.Write("Enter Choice: ")
        menuChoice = Console.ReadLine()

        While menuChoice <> 6
            If menuChoice = 1 Then

                Dim winPercentage(numPlayers) As Decimal
                Dim temp As String
                Dim tempDec As Decimal
                Dim tempS, tempW, tempL As Integer

                Console.WriteLine("Name" & vbTab & vbTab & "Record" & vbTab & vbTab & "Age" & vbTab & "Skill" & vbTab & vbTab & "WinPet")
                Console.WriteLine("====" & vbTab & vbTab & "======" & vbTab & vbTab & "===" & vbTab & "=====" & vbTab & vbTab & "======")

                ' Calculate winning percentage
                For i = 0 To numPlayers - 1
                    If wins(i) = 0 And losses(i) = 0 Then
                        winPercentage(i) = 0
                    Else
                        winPercentage(i) = wins(i) / (wins(i) + losses(i))
                    End If
                Next

                ' Sort by winning percentage
                For i = 0 To numPlayers - 1
                    For j = 0 To numPlayers - 2 - i
                        If winPercentage(j) < winPercentage(j + 1) Then
                            ' swap
                            temp = names(j)
                            names(j) = names(j + 1)
                            names(j + 1) = temp

                            tempW = wins(j)
                            wins(j) = wins(j + 1)
                            wins(j + 1) = tempW

                            tempL = losses(j)
                            losses(j) = losses(j + 1)
                            losses(j + 1) = tempL

                            tempS = skills(j)
                            skills(j) = skills(j + 1)
                            skills(j + 1) = tempS

                            tempDec = winPercentage(j)
                            winPercentage(j) = winPercentage(j + 1)
                            winPercentage(j + 1) = tempDec
                        End If
                    Next
                Next

                For i = 0 To 50 - 1
                    ' print name
                    Console.Write(names(i) & vbTab)

                    ' check for short names
                    If names(i).Length < 8 Then
                        Console.Write(vbTab)
                    End If

                    ' print record
                    Console.Write("(" & wins(i) & "-" & losses(i) & ")" & vbTab)

                    ' check for short records
                    If wins(i).ToString.Length + losses(i).ToString.Length < 5 Then
                        Console.Write(vbTab)
                    End If

                    ' print age
                    initialAge = randomAge()
                    Console.Write(initialAge & vbTab)

                    ' print skill
                    Console.Write(skills(i) & vbTab)

                    ' print win percentage
                    Console.Write(vbTab & winPercentage(i).ToString("P1"))
                    Console.WriteLine()
                Next

            ElseIf menuChoice = 2 Then
                Dim player1, player2, combinedSkill, randNum As Integer

                ' Simulate Matches
                For i = 1 To 500

                    ' find players
                    player1 = Int(Rnd() * numPlayers)
                    player2 = Int(Rnd() * numPlayers)

                    ' code to avoid playing with yourself (joke)
                    While player1 = player2
                        player2 = Int(Rnd() * numPlayers)
                    End While

                    ' get skills
                    combinedSkill = skills(player1) + skills(player2)

                    ' figure out winner and loser
                    randNum = Int(Rnd() * combinedSkill)

                    If randNum < skills(player1) Then
                        ' player 1 wins
                        wins(player1) += 1
                        losses(player2) += 1
                    Else
                        ' player 2 wins
                        wins(player2) += 1
                        losses(player1) += 1
                    End If
                Next

            ElseIf menuChoice = 3 Then
                storeAge = NewPlayer(names, age, skills, numPlayers)

            ElseIf menuChoice = 4 Then
                Dim name As String
                Dim check(numPlayers) As Boolean
                Dim checker As Integer = 0

                ' Find high and low for input validation
                Dim highLow() As Integer = findHighLow(skills, numPlayers)
                Dim low As Integer = highLow(0)
                Dim high As Integer = highLow(1)

                ' Change skills for existing names
                Console.WriteLine("Please enter a name:")
                name = Console.ReadLine()

                For i = 0 To numPlayers - 1
                    If names(i) = name Then
                        skills(i) = validateInput(low, high)
                        checker = checker + 1
                    End If
                Next

                ' Change skills for new names
                If checker = 0 Then
                    names(50 - 1) = name
                End If

                If checker = 0 Then
                    skills(50 - 1) = validateInput(low, high)
                End If

            ElseIf menuChoice = 5 Then
                Dim chance As Decimal
                Dim skill As Integer
                Dim retirement As Boolean
                Dim actualAge As Integer

                For i = 0 To numPlayers - 1
                    

                    Console.WriteLine("SKILL CHANGE: " & actualAge & names(i) & vbTab & "(" & changeSkill & ")")

                    While retirement = True
                        initialAge += actualAge
                        year += year
                        names(i) = shouldRetire(skill, age)
                        Console.WriteLine("RETIREMENT: " & actualAge & names(i))
                        NewPlayer(names, age, skills, numPlayers)
                    End While

                    changeSkill = SkillChange(age)
                    If skills(numPlayers) < 1 Then
                        skills(numPlayers) = 1
                    End If
                Next

            Else
                ' Invalid
                Console.WriteLine("Invalid Entry...")
            End If

            ' pause
            Console.WriteLine("Press any key to continue...")
            Console.ReadKey(True)

            ' clear
            Console.Clear()

            ' menu again
            Console.Write("   Menu ")
            Console.WriteLine("Year " & year)
            Console.WriteLine("=======================")
            Console.WriteLine("1) View Records")
            Console.WriteLine("2) Simulate Matches")
            Console.WriteLine("3) Add New Player")
            Console.WriteLine("4) Change Player's Rank")
            Console.WriteLine("5) Advance to Next Season (Players will Age One Year)")
            Console.WriteLine("6) Exit")
            Console.WriteLine("=======================")
            Console.Write("Enter Choice: ")
            menuChoice = Console.ReadLine()
        End While

    End Sub

    Function loadPlayers(ByRef names() As String, ByRef age As Integer, ByRef skills() As Integer, ByRef wins() As Integer,
                         ByRef losses() As Integer) As Integer
        Dim numPlayers As Integer
        Dim i As Integer
        ' initialize arrays
        names(0) = "Alec"
        names(1) = "John"
        names(2) = "Joshua"
        names(3) = "Brenden"
        names(4) = "Alexander"
        names(5) = "Fnu"
        names(6) = "Ibrahim"
        names(7) = "Kamil"
        names(8) = "Amanda"
        names(9) = "Sakshi"
        names(10) = "Erik"
        names(11) = "Tad"
        names(12) = "Alex"
        names(13) = "Justin"
        names(14) = "Matt"
        names(15) = "Talha"
        names(16) = "Daniel"
        names(17) = "Wyatt"
        names(18) = "Yanhong"
        names(19) = "Chuck Norris"
        names(20) = "Louis"

        numPlayers = 21
        ' add player to 50
        For i = 0 To numPlayers - 1

            ' zero out wins and losses
            wins(i) = 0
            losses(i) = 0

            ' randomly set skill
            skills(i) = Int(Rnd() * 100) + 1

            If names(i) = "Louis" Then
                skills(i) = 101
            ElseIf names(i) = "Chuck Norris" Then
                skills(i) = 2000
            End If
        Next

        Return numPlayers
    End Function

    Function validateInput(ByVal low As Integer, ByVal high As Integer) As Integer
        Dim choice As Integer

        Console.WriteLine("Please input a skill value between " & low & " and " & high)
        choice = Console.ReadLine()

        While choice < low Or choice > high
            Console.WriteLine("Error! Skill value must be between " & low & " and " & high)
            Console.Write("Try Again: ")
            choice = Console.ReadLine()
        End While

        Return choice
    End Function

    Function findHighLow(ByVal skills() As Integer, ByVal numPlayers As Integer) As Integer()
        Dim highLow(2) As Integer
        Dim temp As Integer

        ' Sort skills and store the first and last elements
        For i = 0 To numPlayers - 1
            For j = 0 To numPlayers - 2 - i
                If skills(j) < skills(j + 1) Then
                    ' swap
                    temp = skills(j)
                    skills(j) = skills(j + 1)
                    skills(j + 1) = temp

                End If
            Next
        Next

        highLow(0) = skills(numPlayers - 1)
        highLow(1) = skills(0)

        Return highLow
    End Function

    Function randomAge() As Integer
        Dim age As Integer
        age = Int(Rnd() * 18) + 18
        Return age
    End Function

    Function NewPlayer(ByRef names() As String, ByRef age As Integer, ByRef skills() As Integer, ByRef numPlayers As Integer)

        Dim roster(50) As player

        For i = 0 To numPlayers - 1
            roster(i) = New player
        Next

        ' generate 3 digit playerID
        Dim id As Integer
        id = Int(Rnd() * 800) + 100

        ' add to playerName
        names(numPlayers) = "Player_" & id
        Console.WriteLine("Add New Player Name: " & names(numPlayers))
        roster(0).name = names(numPlayers)

        Console.Write("What is their skill level (1-100): ")
        roster(0).skills = Console.ReadLine()

        Console.Write("What is the new player's age (18-23): ")
        roster(0).age = Console.ReadLine()

        While roster(0).skills < 1 Or roster(0).skills > 100
            Console.WriteLine("Error! Invalid entry...")
            Console.Write("Try Again: ")
            roster(0).skills = Console.ReadLine()
        End While

        While roster(0).age < 18 Or roster(0).age > 23
            Console.WriteLine("Error! Invalid entry...")
            Console.Write("Try Again: ")
            roster(0).age = Console.ReadLine()
        End While
        ' increment numplayers
        numPlayers = numPlayers + 1

        ' print it out
        printPlayer(roster(0))

        Return numPlayers
    End Function

    Sub printPlayer(p As player)
        Console.WriteLine(p.name & vbTab & vbTab & p.record & vbTab & vbTab & p.skills & vbTab & vbTab & p.age & vbTab & vbTab & p.winPercent.ToString("P1"))
    End Sub

    Function shouldRetire(ByVal skill As Integer, ByVal age As Integer) As Boolean
        Dim chance, check As Integer
        chance = 0
        If skill < 30 Then
            If age < 34 Then
                chance = 2
            Else
                chance = (age - 33) * 10
            End If
        End If
        ' Chuck Norris Code
        If age > 50 Then
            chance = (age - 50) * 10
        End If
        check = Int(Rnd() * 100) + 1
        If check < chance Then
            Return True
        Else
            Return False
        End If
    End Function

    Function SkillChange(ByVal age As Integer) As Integer
        Dim increase, decrease As Integer
        Dim actualIncrease, actualDecrease As Integer
        If age <= 27 Then
            increase = age - 8
            decrease = 20 - increase
        ElseIf age = 28 Then
            increase = 15
            decrease = 5
        Else
            decrease = age - 20
            increase = 20 - decrease
            If increase < 0 Then
                increase = 0
            End If
        End If
        actualIncrease = Int(Rnd() * increase)
        actualDecrease = Int(Rnd() * decrease)
        Return actualIncrease - actualDecrease
    End Function

End Module
