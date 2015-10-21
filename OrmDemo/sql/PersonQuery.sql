SELECT p.Id, p.Name, MemberOf_Id, o.Id as orgId, o.Name as oName, o.Amount
FROM People p
JOIN Organizations o on MemberOf_Id = o.Id
