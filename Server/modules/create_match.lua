local nk = require("nakama")
local match_info = require("match_info")

local function join_match(context, payload)
  local decoded = nk.json_decode(payload)

  local limit = 1000
  local authoritative = nil 
  local min_size = 0
  local label = decoded.label
  local max_size = 1000
  local matches = nk.match_list(limit, authoritative, label, min_size, max_size)

  if #matches > 0 then
    for _, m in ipairs(matches)
    do
      print("found matchid")
      print(m.match_id)
      return m.match_id
    end
  end

  local modulename = decoded.modulename
  local setupstate = { initialstate = decoded }
  local matchid = nk.match_create(modulename, setupstate)
  
  print("creating match")
  print(matchid)
  return matchid
end

nk.register_rpc(join_match, "join_match_rpc")
